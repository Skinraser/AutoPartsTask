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
        AutoPartsTaskContext db;
        IConfiguration config;
        IBrowsingContext context;
        SubPartRepository subPartRepository;
        public PrimaryPartRepository()
        {
            db = new AutoPartsTaskContext();
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            subPartRepository = new SubPartRepository();
        }
        public async Task CreatePrimaryPart(string address, int carModelId, int complectationId)
        {
            var document = await context.OpenAsync(address);
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
                        CarModelId = carModelId,
                        ComplectationId = complectationId,
                    };
                    db.PrimaryParts.Add(primaryPart);
                    await db.SaveChangesAsync();
                    await subPartRepository.CreateSubPart(address + cells[i].GetAttribute("href"), primaryPart.Id, complectationId, carModelId);
                }
                await db.SaveChangesAsync();
            }
        }
        public async Task CreatePrimaryPart(string address, int carModelId)
        {
            var document = await context.OpenAsync(address);
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
                        CarModelId = carModelId,
                    };
                    db.PrimaryParts.Add(primaryPart);
                    await db.SaveChangesAsync();
                    await subPartRepository.CreateSubPart(address + cells[i].GetAttribute("href"), primaryPart.Id, carModelId);
                }
                await db.SaveChangesAsync();
            }
        }
    }
}
