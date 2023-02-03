namespace Infrastructure.Persistansce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImageRepository")]
    public partial class ImageRepository
    {
        [Key]
        public int Id_Image { get; set; }

        [Required]
        [StringLength(150)]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string Path { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
