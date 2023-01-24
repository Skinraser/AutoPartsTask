using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Model
{
    public class SubPart
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Revision { get; set; }
        public string? Usage { get; set; }
        public int PrimaryPartId { get; set; }
        public int? ComplectationId { get; set; }
        public int CarModelId { get; set; }
    }
}
