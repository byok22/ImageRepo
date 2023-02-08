namespace Infrastructure.Persistansce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IX_ProcessRoute
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FKProcess { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ProcessRoute { get; set; }
    }
}
