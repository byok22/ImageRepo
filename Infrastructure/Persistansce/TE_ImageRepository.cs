namespace Infrastructure.Persistansce
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class TE_ImageRepository : DbContext
    {
        public TE_ImageRepository()
            : base("TE_ImageRepository")
        {
            Database.SetInitializer<TE_ImageRepository>(null);
        }


        public virtual DbSet<AR_ImageRepository> AR_ImageRepository { get; set; }
        public virtual DbSet<CT_Process> CT_Process { get; set; }
        public virtual DbSet<CT_ServersName> CT_ServersName { get; set; }
        public virtual DbSet<ImageRepository> ImageRepository { get; set; }
        public virtual DbSet<IX_ProcessRoute> IX_ProcessRoute { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<AR_ImageRepository>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AR_ImageRepository>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<CT_Process>()
                .Property(e => e.Process)
                .IsUnicode(false);

            modelBuilder.Entity<CT_ServersName>()
                .Property(e => e.ServerName)
                .IsUnicode(false);

            modelBuilder.Entity<ImageRepository>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ImageRepository>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<IX_ProcessRoute>()
                .Property(e => e.ProcessRoute)
                .IsUnicode(false);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
