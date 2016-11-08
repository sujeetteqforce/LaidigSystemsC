using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaidigSystemsC.Models
{
    public class DataLogAB
    {
        [Key]
        public int Id { get; set; }

        public string LocalDate { get; set; }
        public string LocalTime { get; set; }
        public string LMIG12000 { get; set; }
        public string LMIM12000 { get; set; }
        public string LMIG11301 { get; set; }
        public string LMIG11300 { get; set; }
        public string LMIG11401 { get; set; }
        public string LMIG11400 { get; set; }
        public string LMIP11400 { get; set; }
        public string LMIG10100 { get; set; }
        public string LMIG10101 { get; set; }
        public string LMIG10102 { get; set; }
        public string LMIP10103 { get; set; }
        public string LMIP10104 { get; set; }
        public string LMIF10100 { get; set; }
        public string LMIG11001 { get; set; }
        public string LMIT11001 { get; set; }
        public string LMIT11002 { get; set; }


        
    }

    
}