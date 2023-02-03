namespace Infrastructure.Persistansce
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TE_ImageRepository : DbContext
    {
        public TE_ImageRepository()
            : base("name=TE_ImageRepository")
        {
        }

        public virtual DbSet<ImageRepository> ImageRepository { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageRepository>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ImageRepository>()
                .Property(e => e.Path)
                .IsUnicode(false);
        }
    }
}
