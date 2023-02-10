using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{   
    public class UserActivity
    {
        public int PkUserActivity { get; set; }
        public string UserNT { get; set; }
        public int FkProcess { get; set; }
        public string Activity { get; set; }
        public string Terminal { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
