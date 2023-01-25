using AngleSharp;
using ConsoleApp3.Interfaces;
using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Repositories
{
    public class PrimaryPartRepository : IPrimaryPartRepository
    {
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        ISubPartRepository _subPartRepository;
        public PrimaryPartRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _subPartRepository = new SubPartRepository();
        }
        public async Task Create(string address, Complectation complectation)
        {
            var document = await _context.OpenAsync(address);
            if (document.QuerySelector("h1").TextContent == "Выбор группы запчастей")
            {
                address = "https://www.ilcats.ru";
                var cellSelector = " div.name a";
                var cells = document.QuerySelectorAll(cellSelector);
                for (var i = 0; i < cells.Length; i++)
                {
                    var primaryPart = new PrimaryPart()
                    {
                        Name = cells[i].TextContent,
                        ComplectationId = complectation.Id,
                    };
                    _db.PrimaryParts.Add(primaryPart);
                    await _db.SaveChangesAsync();
                    await _subPartRepository.Create(address + cells[i].GetAttribute("href"), primaryPart);
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
