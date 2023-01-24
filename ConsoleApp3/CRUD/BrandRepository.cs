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
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        string address = "https://www.ilcats.ru";
        MarketRepository _marketRepository;
        public BrandRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _marketRepository = new MarketRepository();
        }
        public async Task CreateBrand()
        {
            var document = await _context.OpenAsync(address);
            var cellSelector = " div.CatalogGroup a";
            var cells = document.QuerySelectorAll(cellSelector);
            for (var i = 1; i<= cells.Length; i++)
            {
                var brand = new Brand()
                {
                    Name = cells[i-1].TextContent
                };
                _db.Brands.Add(brand);
                await _db.SaveChangesAsync();
                await _marketRepository.CreateMarket(address + cells[i-1].GetAttribute("href"), brand);
            }
            await _db.SaveChangesAsync();

        }
    }
}
