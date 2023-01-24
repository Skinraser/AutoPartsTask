using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Model
{
    public class Part
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubPartId { get; set; }
    }
}
