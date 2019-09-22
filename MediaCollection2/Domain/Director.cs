using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class Director
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public int MovieID { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
