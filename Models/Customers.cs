using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace test3.Models
{
    public class Customers
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        public String Name { get; set; }
        public int Price { get; set; }
        public int sale { get; set; }

      
    }
}
