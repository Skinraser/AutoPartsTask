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
    public class CarModelRepository : ICarModelRepository
    {
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        IComplectationRepository _complectationRepository;
        public CarModelRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _complectationRepository = new ComplectationRepository();
        }
        public async Task Create(string address, Market market)
        {
            var document = await _context.OpenAsync(address);
            address = "https://www.ilcats.ru";
            var headerCellSelector = "div.Multilist > div.List";
            var headerCells = document.QuerySelectorAll(headerCellSelector);
            for (var i = 0; i < headerCells.Length; i++)
            {
                for (int j = 0; j <headerCells[i].Children[1].ChildElementCount; j++)
                { 
                    var complectation = headerCells[i].GetElementsByClassName("comment").FirstOrDefault()?.TextContent;
                    var code = headerCells[i].GetElementsByClassName("code").FirstOrDefault()?.TextContent;
                    var carModel = new CarModel()
                    {
                        Model = headerCells[i].Children[0].TextContent,
                        Code = code ??= headerCells[i].Children[1].Children[j].GetElementsByTagName("a").FirstOrDefault()?.TextContent,
                        ProductionYear = headerCells[i].GetElementsByClassName("dateRange").FirstOrDefault()?.TextContent,
                        Type = complectation ??= headerCells[i].GetElementsByClassName("modelCode").FirstOrDefault()?.TextContent,
                        MarketId = market.Id,
                    };
                    _db.CarModels.Add(carModel);
                    await _db.SaveChangesAsync();
                    var adress = headerCells[i].Children[1].Children[j].GetElementsByTagName("a").FirstOrDefault()?.GetAttribute("href");
                    await _complectationRepository.Create(address + headerCells[i].Children[1].Children[j].GetElementsByTagName("a").FirstOrDefault()?.GetAttribute("href"), carModel);
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
