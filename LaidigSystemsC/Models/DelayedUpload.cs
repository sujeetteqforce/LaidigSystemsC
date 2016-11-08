using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaidigSystemsC.Models
{
    public class DelayedUpload
    {
        [Key]
        public int DelayedId { get; set; }        

        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }

   
}