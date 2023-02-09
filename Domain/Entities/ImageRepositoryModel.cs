using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ImageRepositoryModel
    {
        
        public int Id_Image { get; set; }      
        public string SerialNumber { get; set; }   
        public int FKProcess { get; set; } 
        public string Path { get; set; }   
        public DateTime FileDateTime { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
