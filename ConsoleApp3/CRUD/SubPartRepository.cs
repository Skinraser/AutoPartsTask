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
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        PartRepository _partRepository;
        public SubPartRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _partRepository = new PartRepository();
        }

        public async Task CreateSubPart(string address, PrimaryPart primaryPart, Complectation complectation, CarModel carModel)
        {
            var document = await _context.OpenAsync(address);
            address = "https://www.ilcats.ru";
            var cellSelector = "div.name";
            var cells = document.QuerySelectorAll(cellSelector);
            for (var i = 0; i < cells.Length; i++)
            {
                var name = cells[i].TextContent;
                document = await _context.OpenAsync(address + cells[i].Children[0].GetAttribute("href"));
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
                            PrimaryPartId = primaryPart.Id,
                        };
                        _db.SubParts.Add(subPart);
                        await _db.SaveChangesAsync();
                        await _partRepository.CreatePart(address + subCells[j].Children[0].Children[0].GetAttribute("href"), subPart);
                    }
                }
                else
                {
                    var subPart = new SubPart()
                    {
                        Name = name,
                        Revision = cells[i].GetElementsByClassName("revision").FirstOrDefault()?.TextContent,
                        Usage = cells[i].GetElementsByClassName("usage").FirstOrDefault()?.TextContent,
                        PrimaryPartId = primaryPart.Id,
                    };
                    _db.SubParts.Add(subPart);
                    await _db.SaveChangesAsync();
                    await _partRepository.CreatePart(address + cells[i].GetAttribute("href"), subPart);
                }
            }
            await _db.SaveChangesAsync();

        }
    
        public async Task CreateSubPart(string address, PrimaryPart primaryPart, CarModel carModel)
        {
            var document = await _context.OpenAsync(address);
            address = "https://www.ilcats.ru";
            var cellSelector = " div.name a";
            var cells = document.QuerySelectorAll(cellSelector);
            for (var i = 0; i < cells.Length; i++)
            {
                var name = cells[i].TextContent;
                document = await _context.OpenAsync(address + cells[i].GetAttribute("href"));
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
                            PrimaryPartId = primaryPart.Id,
                        };
                        _db.SubParts.Add(subPart);
                        await _db.SaveChangesAsync();
                        await _partRepository.CreatePart(address + subCells[j].Children[0].Children[0].GetAttribute("href"), subPart.Id, primaryPart.Id, carModel.Id);
                    }
                }
                else
                {
                    var subPart = new SubPart()
                    {
                        Name = name,
                        Revision = cells[i].GetElementsByClassName("revision").FirstOrDefault()?.TextContent,
                        Usage = cells[i].GetElementsByClassName("usage").FirstOrDefault()?.TextContent,
                        PrimaryPartId = primaryPart.Id,
                    };
                    _db.SubParts.Add(subPart);
                    await _db.SaveChangesAsync();
                    await _partRepository.CreatePart(address + cells[i].GetAttribute("href"), subPart.Id, primaryPart.Id, carModel.Id);
                }
            }
            await _db.SaveChangesAsync(); 
            
        }
    }
}
