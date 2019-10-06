using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.Series
{
    public class Serie
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public ICollection<Season> Season { get; set; }

    }
}
