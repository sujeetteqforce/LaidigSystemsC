using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaidigSystemsC.Models
{
    public class DataLogMonitouch
    {
        [Key]
        public int Id { get; set; }

        public string TimeDate { get; set; }

        public string G12000 { get; set; }

        public string M12000 { get; set; }

        public string G11301 { get; set; }

        public string G11300 { get; set; }

        public string G11401 { get; set; }

        public string G11400 { get; set; }

        public string P11400 { get; set; }

        public string G10100 { get; set; }

        public string G10101 { get; set; }

        public string G10102 { get; set; }

        public string P10103 { get; set; }

        public string P10104 { get; set; }

        public string F10100 { get; set; }

        public string G11001 { get; set; }

        public string T11001 { get; set; }

        public string T11002 { get; set; }

    }
}