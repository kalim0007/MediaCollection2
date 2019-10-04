using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.MusicModels.MusicWriter
{
    public class MusicWriterViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
        public int MusicID { get; set; }
        public string Music { get; set; }
    }
}
