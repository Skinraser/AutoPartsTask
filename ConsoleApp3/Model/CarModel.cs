using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Model
{
    public class CarModel
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public string Code { get; set; }
        public string? ProductionYear { get; set; }
        public string? Type { get; set; }
        public int BrandId { get; set; }
        public int? MarketId { get; set; }
        public ICollection<Complectation> Complectations { get; set; }
    }
}
