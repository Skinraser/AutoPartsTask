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
        AutoPartsTaskContext db;
        IConfiguration config;
        IBrowsingContext context;
        ComplectationRepository complectationRepository;
        public CarModelRepository()
        {
            db = new AutoPartsTaskContext();
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            complectationRepository = new ComplectationRepository();
        }
        public async Task CreateCarModel(string address, Market market, int brandId)
        {
            var document = await context.OpenAsync(address);
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
                    db.CarModels.Add(carModel);
                    await db.SaveChangesAsync();
                    var adress = headerCells[i - 1].Children[1].Children[j].Children[0].Children[0].GetAttribute("href");
                    await complectationRepository.CreateComplectation(address + headerCells[i - 1].Children[1].Children[j].Children[0].Children[0].GetAttribute("href"), carModel.Id);
                }
            }
            await db.SaveChangesAsync();
        }
        public async Task CreateCarModel(string address, int brandId)
        {
            var document = await context.OpenAsync(address);
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
                    db.CarModels.Add(carModel);
                    await db.SaveChangesAsync();
                    await complectationRepository.CreateComplectation(address + headerCells[i - 1].Children[1].Children[j].Children[0].Children[0].GetAttribute("href"), carModel.Id);
                }
            }
            await db.SaveChangesAsync();
        }
    }
}
