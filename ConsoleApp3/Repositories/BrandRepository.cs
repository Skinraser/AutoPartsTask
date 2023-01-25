using AngleSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using ConsoleApp3.Model;
using ConsoleApp3.Interfaces;

namespace ConsoleApp3.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        IMarketRepository _marketRepository;
        public BrandRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _marketRepository = new MarketRepository();
        }

        // Creation of Brand Entity 
        public async Task Create(string address)
        {
            var document = await _context.OpenAsync(address);
            var cellSelector = " div.CatalogGroup a";
            var cells = document.QuerySelectorAll(cellSelector);
            for (var i = 1; i<= cells.Length; i++) // Iterations through brands on website
            {
                var brand = new Brand()
                {
                    Name = cells[i-1].TextContent
                };
                _db.Brands.Add(brand);
                await _db.SaveChangesAsync();
                await _marketRepository.Create(address + cells[i-1].GetAttribute("href"), brand); // Calling method of creation of the Market entity for brand
            }
            await _db.SaveChangesAsync();

        }
    }
}
