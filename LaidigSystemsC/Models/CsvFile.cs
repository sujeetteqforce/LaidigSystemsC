using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace LaidigSystemsC.Models
{
    public class CsvFile
    {

        [Key]
        public int Id { get; set; }



        [Display(Name = "Date&Time")]
        public string Date { get; set; }


        [Display(Name = "LMI Mode")]
        public string G10001 { get; set; }

        [Display(Name = "Reclaim Pressure A (At HPU)")]
        public string P10100 { get; set; }

        [Display(Name = "Reclaim Pressure B (At HPU)")]
        public string P10101 { get; set; }

        [Display(Name = "Reclaim Pressure A (At Center)")]
        public string P11100 { get; set; }

        [Display(Name = "Advance Pressure A (At HPU)")]
        public string P10200 { get; set; }

        [Display(Name = "Advance Pressure B (At HPU)")]
        public string P10201 { get; set; }

        [Display(Name = "HPU Charge Pressure")]
        public string P10004 { get; set; }

        [Display(Name = "HPU Oil Temp")]
        public string T10000 { get; set; }

        [Display(Name = "Pump Current")]
        public string G10101 { get; set; }

        [Display(Name = "Reclaim Valve mA x10")]
        public string G103001 { get; set; }


        [Display(Name = "Reclaim Bearing Temperature")]
        public string T101100 { get; set; }

        [Display(Name = "Gearbox Temp")]
        public string T10101 { get; set; }

        [Display(Name = "DX80 Ambient Temp")]
        public string T10005 { get; set; }

        [Display(Name = "Reclaim – A1")]
        public string F101000 { get; set; }

        [Display(Name = "Advance – A2")]
        public string F102000 { get; set; }

        [Display(Name = "Dome Door – Position")]
        public string G115001 { get; set; }


        [Display(Name = "Prox Switch – Open")]
        public string G115011 { get; set; }

        [Display(Name = "Machine Parameter 70")]
        public string M12070 { get; set; }

        [Display(Name = "Machine Parameter 71")]
        public string M12071 { get; set; }

        [Display(Name = "Machine Parameter 72")]
        public string M12072 { get; set; }




    }
}