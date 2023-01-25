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
    public class MarketRepository : IMarketRepository
    {
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        ICarModelRepository _carModelRepository;
        
        public MarketRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _carModelRepository = new CarModelRepository();
        }
        // Creation of Market entity
        public async Task Create(string address, Brand brand)
        {
            var document = await _context.OpenAsync(address);
            if (document.QuerySelector("h1").TextContent == "Выбор рынка")
            {
                address = "https://www.ilcats.ru";
                var cellSelector = " div.name a";
                var cells = document.QuerySelectorAll(cellSelector);
                for (var i = 1; i<= cells.Length; i++) // Iteration through markets of certain brand
                {
                    var market = new Market()
                    {
                        Name = brand.Name + " " + cells[i-1].TextContent,
                        BrandId = brand.Id
                    };
                    _db.Markets.Add(market);
                    await _db.SaveChangesAsync();
                    await _carModelRepository.Create(address + cells[i - 1].GetAttribute("href"), market);
                }
                await _db.SaveChangesAsync();
            }
            else // If brand doesn't have markets, code moves to this place and create single market corresponding with certain brand 
            {
                var market = new Market()
                {
                    Name = brand.Name,
                    BrandId = brand.Id
                };
                _db.Markets.Add(market);
                await _db.SaveChangesAsync();
                await _carModelRepository.Create(address, market);
            }
        }
    }
}
