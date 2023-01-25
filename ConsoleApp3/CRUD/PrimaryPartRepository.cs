using AngleSharp;
using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.CRUD
{
    public class PrimaryPartRepository
    {
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        SubPartRepository _subPartRepository;
        public PrimaryPartRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _subPartRepository = new SubPartRepository();
        }
        public async Task CreatePrimaryPart(string address, Complectation complectation)
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
                    await _subPartRepository.CreateSubPart(address + cells[i].GetAttribute("href"), primaryPart);
                }
                await _db.SaveChangesAsync();
            }
        }
        //public async Task CreatePrimaryPart(string address, Complectation complectation)
        //{
        //    var document = await _context.OpenAsync(address);
        //    if (document.QuerySelector("h1").TextContent == "Выбор группы запчастей")
        //    {
        //        address = "https://www.ilcats.ru";
        //        var cellSelector = " div.name a";
        //        var cells = document.QuerySelectorAll(cellSelector);
        //        for (var i = 0; i < cells.Length; i++)
        //        {
        //            var primaryPart = new PrimaryPart()
        //            {
        //                Name = cells[i].TextContent,
        //            };
        //            _db.PrimaryParts.Add(primaryPart);
        //            await _db.SaveChangesAsync();
        //            await _subPartRepository.CreateSubPart(address + cells[i].GetAttribute("href"), primaryPart);
        //        }
        //        await _db.SaveChangesAsync();
        //    }
        //}
    }
}
