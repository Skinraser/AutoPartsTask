using AngleSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using ConsoleApp3.Model;

namespace ConsoleApp3.CRUD
{
    public class BrandRepository
    {
        AutoPartsTaskContext db;
        IConfiguration config;
        IBrowsingContext context;
        string address = "https://www.ilcats.ru";
        MarketRepository _marketRepository;
        public BrandRepository()
        {
            db = new AutoPartsTaskContext();
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            _marketRepository = new MarketRepository();
        }
        public async Task CreateBrand()
        {
            
            var document = await context.OpenAsync(address);
            var cellSelector = " div.CatalogGroup a";
            var cells = document.QuerySelectorAll(cellSelector);
            for (var i = 1; i<= 4; i++)
            {
                var brand = new Brand()
                {
                    Name = cells[i-1].TextContent
                };
                db.Brands.Add(brand);
                await db.SaveChangesAsync();
                await _marketRepository.CreateMarket(address + cells[i-1].GetAttribute("href"), brand);
            }
            await db.SaveChangesAsync();

        }
    }
}
