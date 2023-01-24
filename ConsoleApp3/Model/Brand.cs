﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp3.Model
{
    public class Brand
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Market> Markets { get; set; }
        ICollection<CarModel> CarModels { get; set; }
    }
}