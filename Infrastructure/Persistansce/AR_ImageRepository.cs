namespace Infrastructure.Persistansce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("AR_ImageRepository")]
    public partial class AR_ImageRepository
    {
        [Key]
        public int PKImage { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }

        public int FKProcess { get; set; }

        [Required]
        public string Path { get; set; }

        public DateTime FileDateTime { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
