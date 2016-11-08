using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaidigSystemsC.Models
{
    public class FileDetail
    {
        [Key]
        public Guid Id { get; set; }
            public string FileName { get; set; }
            public string Extension { get; set; }
            public int DelayedId { get; set; }

            public virtual DelayedUpload DelayedUpload { get; set; }

        
    }
}