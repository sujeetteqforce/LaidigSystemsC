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

             
        public string DateofTime { get; set; }

        public string CurSysRateCFM { get; set; }

        public string ReqRecRateCFM { get; set; }

        public string RecSpeedRPM { get; set; }

        public string RecTorque { get; set; }

        public string RecPressurePSI { get; set; }

        public string RecFlowGPM { get; set; }

        public string RecPowerHP { get; set; }

        public string AdvSpeedDPH { get; set; }

        public string AdvTorque { get; set; }

        public string AdvPressurePSI { get; set; }

        public string Disc1Torq { get; set; }

        public string Disc1SpeedRPM { get; set; }

        public string Disc1PowerHP { get; set; }

        public string Disc2Torq { get; set; }

        public string Disc2SpeedRPM { get; set; }

        public string Disc2PowerHP { get; set; }


    }
}