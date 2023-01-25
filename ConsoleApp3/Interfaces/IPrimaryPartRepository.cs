using ConsoleApp3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Interfaces
{
    public interface IPrimaryPartRepository
    {
        public Task Create(string address, Complectation complectation);
    }
}
