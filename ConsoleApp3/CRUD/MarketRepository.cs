using AngleSharp;
using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.CRUD
{
    public class MarketRepository
    {
        AutoPartsTaskContext db;
        IConfiguration config;
        IBrowsingContext context;
        CarModelRepository carModelRepository;
        
        public MarketRepository()
        {
            db = new AutoPartsTaskContext();
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            carModelRepository = new CarModelRepository();
        }
        public async Task CreateMarket(string address, Brand brand)
        {
            var document = await context.OpenAsync(address);
            if (document.QuerySelector("h1").TextContent == "Выбор рынка")
            {
                address = "https://www.ilcats.ru";
                var cellSelector = " div.name a";
                var cells = document.QuerySelectorAll(cellSelector);
                for (var i = 1; i<= cells.Length; i++)
                {
                    var market = new Market()
                    {
                        Name = brand.Name + " " + cells[i-1].TextContent,
                        BrandId = brand.Id
                    };
                    db.Markets.Add(market);
                    await db.SaveChangesAsync();
                    await carModelRepository.CreateCarModel(address + cells[i - 1].GetAttribute("href"), market, brand.Id);
                }
                await db.SaveChangesAsync();
            }
            else
            {
                await carModelRepository.CreateCarModel(address, brand.Id);
            }
        }
    }
}
