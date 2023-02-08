namespace Infrastructure.Persistansce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_Process
    {
        [Key]
        public int PKProcess { get; set; }

        [Required]
        [StringLength(50)]
        public string Process { get; set; }

        public bool Available { get; set; }
    }
}
