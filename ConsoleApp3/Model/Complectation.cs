using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Model
{
    public class Complectation
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public string? Engine { get; set; }
        public string? Body { get; set; }
        public string? Grade { get; set; }
        public string? ATM { get; set; }
        public string? GearShiftType { get; set; }
        public string? Cab { get; set; }
        public string? TransmissionModel { get; set; }
        public string? LoadingCapacity { get; set; }
        public string? RearTire { get; set; }
        public string? Destination { get; set; }
        public string? FuelInduction { get; set; }
        public string? BuildingCondition { get; set; }
        public string? DoorsCount   { get; set; }
        public string? Transmission { get; set; }
        public int CarModelId { get; set; }
        public ICollection<PrimaryPart> PrimaryParts { get; set; }
    }
}
