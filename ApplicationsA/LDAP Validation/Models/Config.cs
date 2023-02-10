using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationsA.LDAP_Validation.Models
{
    public class Config
    {
        public string Parser { get; set; }
        public string TsvPath { get; set; }
        public string Customer { get; set; }
        public string Division { get; set; }
        public string TesterName { get; set; }
        public string TestesProcess { get; set; }
        public string SendToMES { get; set; }
        public string WebService_MES { get; set; }
        public string WebService_Autentication1 { get; set; }
        public string WebService_Autentication2 { get; set; }
    }
}
