namespace Infrastructure.Persistansce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_ServersName
    {
        [Key]
        public int PKServerName { get; set; }

        [Required]
        [StringLength(200)]
        public string ServerName { get; set; }

        public bool Available { get; set; }
    }
}
