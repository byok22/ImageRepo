using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Functions
{
    public static class FileFunctions
    {
        public static bool checkIfSourceExists(string source)
        {
            bool sourceExists = false;
            if (System.IO.Directory.Exists(source))
            {
                sourceExists = true;
            }
            return sourceExists;
        }
        public static bool checkIfTargetExists(string target)
        {
            bool targetExists = false;
            if (System.IO.Directory.Exists(target))
            {
                targetExists = true;
            }
            return targetExists;
        }

    }
}
