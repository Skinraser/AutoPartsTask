using AngleSharp;
using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.CRUD
{
    public class CarModelRepository
    {
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        ComplectationRepository _complectationRepository;
        public CarModelRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _complectationRepository = new ComplectationRepository();
        }
        public async Task CreateCarModel(string address, Market market, int brandId)
        {
            var document = await _context.OpenAsync(address);
            address = "https://www.ilcats.ru";
            var headerCellSelector = "div.Multilist > div.List";
            var headerCells = document.QuerySelectorAll(headerCellSelector);
            for (var i = 1; i <= headerCells.Length; i++)
            {
                for (int j = 0; j < headerCells[i-1].Children[1].ChildElementCount; j++)
                {
                    var complectation = headerCells[i - 1].Children[1].Children[j].GetElementsByClassName("comment").FirstOrDefault()?.TextContent;
                    var carModel = new CarModel()
                    {
                        Model = headerCells[i - 1].Children[0].TextContent,
                        Code = headerCells[i - 1].Children[1].Children[j].Children[0].TextContent,
                        ProductionYear = headerCells[i - 1].Children[1].Children[j].GetElementsByClassName("dateRange").FirstOrDefault()?.TextContent,
                        Type = complectation ??= headerCells[i - 1].Children[1].Children[j].GetElementsByClassName("modelCode").FirstOrDefault()?.TextContent, 
                        MarketId = market.Id,
                        BrandId = brandId
                    };
                    _db.CarModels.Add(carModel);
                    await _db.SaveChangesAsync();
                    var adress = headerCells[i - 1].Children[1].Children[j].Children[0].Children[0].GetAttribute("href");
                    await _complectationRepository.CreateComplectation(address + headerCells[i - 1].Children[1].Children[j].Children[0].Children[0].GetAttribute("href"), carModel.Id);
                }
            }
            await _db.SaveChangesAsync();
        }
        public async Task CreateCarModel(string address, int brandId)
        {
            var document = await _context.OpenAsync(address);
            address = "https://www.ilcats.ru";
            var headerCellSelector = "div.Multilist > div.List";
            var headerCells = document.QuerySelectorAll(headerCellSelector);
            for (var i = 1; i <= headerCells.Length; i++)
            {
                for (int j = 0; j < headerCells[i - 1].Children[1].ChildElementCount; j++)
                {
                    var complectation = headerCells[i - 1].Children[1].Children[j].GetElementsByClassName("comment").FirstOrDefault()?.TextContent;
                    var carModel = new CarModel()
                    {
                        Model = headerCells[i - 1].Children[0].TextContent,
                        Code = headerCells[i - 1].Children[1].Children[j].Children[0].TextContent,
                        ProductionYear = headerCells[i - 1].Children[1].Children[j].GetElementsByClassName("dateRange").FirstOrDefault()?.TextContent,
                        Type = complectation ??= headerCells[i - 1].Children[1].Children[j].GetElementsByClassName("modelCode").FirstOrDefault()?.TextContent,
                        BrandId = brandId
                    };
                    _db.CarModels.Add(carModel);
                    await _db.SaveChangesAsync();
                    await _complectationRepository.CreateComplectation(address + headerCells[i - 1].Children[1].Children[j].Children[0].Children[0].GetAttribute("href"), carModel.Id);
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
