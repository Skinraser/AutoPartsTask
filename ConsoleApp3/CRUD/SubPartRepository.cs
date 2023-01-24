using AngleSharp;
using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.CRUD
{
    public class SubPartRepository
    {
        AutoPartsTaskContext db;
        IConfiguration config;
        IBrowsingContext context;
        PartRepository partRepository;
        public SubPartRepository()
        {
            db = new AutoPartsTaskContext();
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            partRepository = new PartRepository();
        }

        public async Task CreateSubPart(string address, int primaryPartId, int complectationId, int carModelId)
        {
            var document = await context.OpenAsync(address);
            address = "https://www.ilcats.ru";
            var cellSelector = "div.name";
            var cells = document.QuerySelectorAll(cellSelector);
            for (var i = 0; i < cells.Length; i++)
            {
                var name = cells[i].TextContent;
                document = await context.OpenAsync(address + cells[i].GetAttribute("href"));
                if (document.QuerySelector("h1").TextContent == "Выбор подгруппы запчастей")
                {
                    var subCells = document.QuerySelectorAll("div.List > div.List");
                    for (int j = 0; j < subCells.Length; j++)
                    {
                        var subPart = new SubPart()
                        {
                            Name = name,
                            Revision = subCells[j].GetElementsByClassName("revision").FirstOrDefault()?.TextContent,
                            Usage = subCells[j].GetElementsByClassName("usage").FirstOrDefault()?.TextContent,
                            CarModelId = carModelId,
                            PrimaryPartId = primaryPartId,
                            ComplectationId = complectationId
                        };
                        db.SubParts.Add(subPart);
                        await db.SaveChangesAsync();
                        await partRepository.CreatePart(address + subCells[j].Children[0].Children[0].GetAttribute("href"), subPart.Id, primaryPartId, carModelId);
                    }
                }
                else
                {
                    var subPart = new SubPart()
                    {
                        Name = name,
                        Revision = cells[i].GetElementsByClassName("revision").FirstOrDefault()?.TextContent,
                        Usage = cells[i].GetElementsByClassName("usage").FirstOrDefault()?.TextContent,
                        CarModelId = carModelId,
                        PrimaryPartId = primaryPartId,
                        ComplectationId= complectationId
                    };
                    db.SubParts.Add(subPart);
                    await db.SaveChangesAsync();
                    await partRepository.CreatePart(address + cells[i].GetAttribute("href"), subPart.Id, primaryPartId, carModelId);
                }
            }
            await db.SaveChangesAsync();

        }
    
        public async Task CreateSubPart(string address, int primaryPartId, int carModelId)
        {
            var document = await context.OpenAsync(address);
            address = "https://www.ilcats.ru";
            var cellSelector = " div.name a";
            var cells = document.QuerySelectorAll(cellSelector);
            for (var i = 0; i < cells.Length; i++)
            {
                var name = cells[i].TextContent;
                document = await context.OpenAsync(address + cells[i].GetAttribute("href"));
                if(document.QuerySelector("h1").TextContent == "Выбор подгруппы запчастей")
                {
                    var subCells = document.QuerySelectorAll("div.List > div.List");
                    for (int j = 0; j < subCells.Length; j++)
                    {
                        var subPart = new SubPart()
                        {
                            Name = name,
                            Revision = subCells[j].GetElementsByClassName("revision").FirstOrDefault()?.TextContent,
                            Usage = subCells[j].GetElementsByClassName("usage").FirstOrDefault()?.TextContent,
                            CarModelId = carModelId,
                            PrimaryPartId = primaryPartId,
                        };
                        db.SubParts.Add(subPart);
                        await db.SaveChangesAsync();
                        await partRepository.CreatePart(address + subCells[j].Children[0].Children[0].GetAttribute("href"), subPart.Id, primaryPartId, carModelId);
                    }
                }
                else
                {
                    var subPart = new SubPart()
                    {
                        Name = name,
                        Revision = cells[i].GetElementsByClassName("revision").FirstOrDefault()?.TextContent,
                        Usage = cells[i].GetElementsByClassName("usage").FirstOrDefault()?.TextContent,
                        CarModelId = carModelId,
                        PrimaryPartId = primaryPartId,
                    };
                    db.SubParts.Add(subPart);
                    await db.SaveChangesAsync();
                    await partRepository.CreatePart(address + cells[i].Children[0].Children[0].GetAttribute("href"), subPart.Id, primaryPartId, carModelId);
                }
            }
            await db.SaveChangesAsync(); 
            
        }
    }
}
