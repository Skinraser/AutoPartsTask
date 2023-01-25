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
    public class ComplectationRepository : IComplectationRepository
    {
        AutoPartsTaskContext _db;
        IConfiguration _config;
        IBrowsingContext _context;
        IPrimaryPartRepository _primaryPartRepository;
        public ComplectationRepository()
        {
            _db = new AutoPartsTaskContext();
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);
            _primaryPartRepository = new PrimaryPartRepository();
        }
        public async Task Create(string address, CarModel carModel)
        {
            var document = await _context.OpenAsync(address);
            if (document.QuerySelector("h1").TextContent == "Выбор комплектации автомобиля" || document.QuerySelector("h1").TextContent == "Выбор модификации")
            {
                address = "https://www.ilcats.ru";
                var cellSelector = " tr";
                var cells = document.QuerySelectorAll(cellSelector);
                if (cells.Length == 0)
                {
                    cellSelector = " div.name";
                    cells = document.QuerySelectorAll(cellSelector);
                    for (int k = 1; k < cells.Length; k++)
                    {
                        var complectation = new Complectation()
                        {
                            Name = cells[k].TextContent,
                            CarModelId = carModel.Id,
                        };
                        _db.Complectations.Add(complectation);
                        await _db.SaveChangesAsync();
                        await _primaryPartRepository.Create(address + cells[k].Children[0].GetAttribute("href"), complectation);
                    }
                }
                else
                {

                    for (var i = 1; i < cells.Length; i++)
                    {
                        var name = cells[i].GetElementsByClassName("modelCode").FirstOrDefault()?.TextContent;
                        var complectation = new Complectation()
                        {
                            Name = name ??= cells[i].GetElementsByClassName("name").FirstOrDefault()?.TextContent,
                            Date = cells[i].GetElementsByClassName("dateRange").FirstOrDefault()?.TextContent,
                            Engine = cells[i].GetElementsByClassName("01").FirstOrDefault()?.TextContent,
                            Body = cells[i].GetElementsByClassName("03").FirstOrDefault()?.TextContent,
                            Grade = cells[i].GetElementsByClassName("04").FirstOrDefault()?.TextContent,
                            ATM = cells[i].GetElementsByClassName("05").FirstOrDefault()?.TextContent,
                            GearShiftType = cells[i].GetElementsByClassName("06").FirstOrDefault()?.TextContent,
                            Cab = cells[i].GetElementsByClassName("07").FirstOrDefault()?.TextContent,
                            TransmissionModel = cells[i].GetElementsByClassName("08").FirstOrDefault()?.TextContent,
                            LoadingCapacity = cells[i].GetElementsByClassName("09").FirstOrDefault()?.TextContent,
                            RearTire = cells[i].GetElementsByClassName("10").FirstOrDefault()?.TextContent,
                            Destination = cells[i].GetElementsByClassName("11").FirstOrDefault()?.TextContent,
                            FuelInduction = cells[i].GetElementsByClassName("12").FirstOrDefault()?.TextContent,
                            BuildingCondition = cells[i].GetElementsByClassName("13").FirstOrDefault()?.TextContent,
                            DoorsCount = cells[i].GetElementsByClassName("doorsCount").FirstOrDefault()?.TextContent,
                            Transmission = cells[i].GetElementsByClassName("transmission").FirstOrDefault()?.TextContent,
                            CarModelId = carModel.Id,

                        };
                        _db.Complectations.Add(complectation);
                        await _db.SaveChangesAsync();
                        await _primaryPartRepository.Create(address + cells[i].GetElementsByTagName("a").FirstOrDefault()?.GetAttribute("href"), complectation);

                    }
                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                var complectation = new Complectation()
                {
                    Name = carModel.Code,
                    CarModelId = carModel.Id,
                };
                _db.Complectations.Add(complectation);
                await _db.SaveChangesAsync();
                await _primaryPartRepository.Create(address, complectation);
            }
        }
    }
}
