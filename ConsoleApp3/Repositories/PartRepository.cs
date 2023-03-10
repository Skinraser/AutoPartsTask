using AngleSharp;
using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Repositories
{
    public class PartRepository
    {
        AutoPartsTaskContext db;
        IConfiguration config;
        IBrowsingContext context;
        public PartRepository()
        {
            db = new AutoPartsTaskContext();
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
        }
        public async Task CreatePart(string address, SubPart subPart)
        {

        }
        public async Task CreatePart(string address, int subPartId, int primaryPartId, int carModelId)
        {

        }
    }
}
