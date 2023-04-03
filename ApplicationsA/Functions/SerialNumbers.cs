using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationsA.Functions
{
    public static class SerialNumbers
    {
        public static string GetValidSeriaFromPath(string completePath,string process)
        {
            string FileName = Path.GetFileName(completePath);
            //The serial number starts with FE2 then 5 digits, then A then first leter of orientation(horizontal or vertical) then any digit  
            if (process.ToUpper() == "EOL")
            {                
                return Regex.Match(FileName, @"FE2\d{5}\d[C|V]\d[0-9]+").Value;                
            }
            string serialNumber = Regex.Match(FileName, @"FE2\d{5}A[HV]\d[0-9]+").Value;
            return serialNumber;
        }
        public static string GetValidSerialNumberFromPath(string completePath,string process)
        {
            string FileName = Path.GetFileName(completePath);
            if (process.ToUpper() == "EOL")
            {
                return Regex.Match(FileName, @"FE2\d{5}\d[C|V]\d[0-9]+").Value!=""? completePath:"";
            }
            //The serial number starts with FE2 then 5 digits, then A then first leter of orientation(horizontal or vertical) then any digit  
            string serialNumber = Regex.Match(FileName, @"FE2\d{5}A[HV]\d[0-9]+").Value;
            return serialNumber!=""? completePath : "";
        }

    }
}
