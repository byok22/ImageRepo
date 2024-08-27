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

            if (process.ToUpper() == "IC")
            {
                return Regex.Match(FileName, @"[A-Z]\d[0-9]+-0[A-Z]-[A-Z]_SFE2\d{5}\d[C|V]\d[0-9]+").Value;
            }
            if (process.ToUpper() == "AA")
            {
                var reg1 = Regex.Match(FileName, @"FE2\d{5}A[HV]\d[0-9]+").Value;
                var reg2 = Regex.Match(FileName, @"[A-Z]\d+-[0-9A-Z]+-[A-Z]_SFE\d+[CV]\d+").Value;
                return reg1 != null && reg1 != "" ? reg1 : reg2??"";
                    
            }
            if (process.ToUpper() == "EOL")
            {
                return Regex.Match(FileName, @"[A-Z]\d[0-9]+-0[A-Z]-[A-Z]_SFE2\d{5}\d[C|V]\d[0-9]+").Value;
            }
            //The serial number starts with FE2 then 5 digits, then A then first leter of orientation(horizontal or vertical) then any digit  
            string serialNumber = Regex.Match(FileName, @"FE2\d{5}A[HV]\d[0-9]+").Value;
            return serialNumber;
        }
        public static string GetValidSerialNumberFromPath(string completePath,string process)
        {
            string serialNumber = "";
            string FileName = Path.GetFileName(completePath);
            if ( process.ToUpper() == "IC")
            {
                serialNumber= Regex.Match(FileName, @"[A-Z]\d[0-9]+-0[A-Z]-[A-Z]_SFE2\d{5}\d[C|V]\d[0-9]+").Value!=""? completePath:"";
            }else
            if (process.ToUpper() == "AA")
            {
                serialNumber = (
                    Regex.Match(FileName, @"FE2\d{5}A[HV]\d[0-9]+").Value != "" ||  
                    Regex.Match(FileName, @"[A-Z]\d+-[0-9A-Z]+-[A-Z]_SFE\d+[CV]\d+").Value != ""

                    )
                     ? completePath : "";
            }
            else
            if (process.ToUpper() == "EOL" )
            {
                serialNumber = Regex.Match(FileName, @"[A-Z]\d[0-9]+-0[A-Z]-[A-Z]_SFE2\d{5}\d[C|V]\d[0-9]+").Value != "" ? completePath : "";
            }
            else
            {
                serialNumber = Regex.Match(FileName, @"FE2\d{5}A[HV]\d[0-9]+").Value;
            }
            //The serial number starts with FE2 then 5 digits, then A then first leter of orientation(horizontal or vertical) then any digit  
            
            return serialNumber!=""? completePath : "";
        }

    }
}
