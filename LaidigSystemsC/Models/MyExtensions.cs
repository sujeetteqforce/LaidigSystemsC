using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LaidigSystemsC.Models
{
    public static class MyExtensions
    {
        public static string AppendTimeStamp(this string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }
    }
}