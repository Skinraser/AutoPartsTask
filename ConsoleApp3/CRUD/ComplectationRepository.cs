using AngleSharp;
using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.CRUD
{
    public class ComplectationRepository
    {
        AutoPartsTaskContext db;
        IConfiguration config;
        IBrowsingContext context;
        PrimaryPartRepository primaryPartRepository;
        public ComplectationRepository()
        {
            db = new AutoPartsTaskContext();
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            primaryPartRepository = new PrimaryPartRepository();
        }
        public async Task CreateComplectation(string address, int carModelId)
        {
            var document = await context.OpenAsync(address);
            if (document.QuerySelector("h1").TextContent == "Выбор комплектации автомобиля" || document.QuerySelector("h1").TextContent == "Выбор модификации")
            {
                address = "https://www.ilcats.ru";
                var cellSelector = " tbody:first-child";
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
                            CarModelId = carModelId,
                        };
                        db.Complectations.Add(complectation);
                        await db.SaveChangesAsync();
                        await primaryPartRepository.CreatePrimaryPart(address + cells[k].Children[0].GetAttribute("href"), carModelId, complectation.Id);
                    }
                }
                else
                {

                    for (var i = 0; i < cells.Length - 1; i++)
                    {
                        for (int j = 1; j < cells[0].ChildElementCount; j++)
                        {
                            var complectation = new Complectation()
                            {
                                Name = cells[i].Children[j].Children[0].TextContent,
                                Date = cells[i].Children[j].GetElementsByClassName("dateRange").FirstOrDefault()?.TextContent,
                                Engine = cells[i].Children[j].GetElementsByClassName("01").FirstOrDefault()?.TextContent,
                                Body = cells[i].Children[j].GetElementsByClassName("03").FirstOrDefault()?.TextContent,
                                Grade = cells[i].Children[j].GetElementsByClassName("04").FirstOrDefault()?.TextContent,
                                ATM = cells[i].Children[j].GetElementsByClassName("05").FirstOrDefault()?.TextContent,
                                GearShiftType = cells[i].GetElementsByClassName("06").FirstOrDefault()?.TextContent,
                                Cab = cells[i].Children[j].GetElementsByClassName("07").FirstOrDefault()?.TextContent,
                                TransmissionModel = cells[i].GetElementsByClassName("08").FirstOrDefault()?.TextContent,
                                LoadingCapacity = cells[i].GetElementsByClassName("09").FirstOrDefault()?.TextContent,
                                RearTire = cells[i].Children[j].GetElementsByClassName("10").FirstOrDefault()?.TextContent,
                                Destination = cells[i].Children[j].GetElementsByClassName("11").FirstOrDefault()?.TextContent,
                                FuelInduction = cells[i].Children[j].GetElementsByClassName("12").FirstOrDefault()?.TextContent,
                                BuildingCondition = cells[i].Children[j].GetElementsByClassName("13").FirstOrDefault()?.TextContent,
                                CarModelId = carModelId,

                            };
                            db.Complectations.Add(complectation);
                            await primaryPartRepository.CreatePrimaryPart(address, carModelId, complectation.Id);
                            await db.SaveChangesAsync();
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            else
            {
                await primaryPartRepository.CreatePrimaryPart(address, carModelId);
            }
        }
    }
}
