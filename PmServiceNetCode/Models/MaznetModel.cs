using Microsoft.EntityFrameworkCore;
using PmServiceNetCode.Models;


namespace pmService.Models
{
    public partial class MaznetModel : DbContext
    {
        public MaznetModel(DbContextOptions<MaznetModel> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TblFarayand> TblFarayand { get; set; }
        public virtual DbSet<tbl_Catout> tbl_Catout { get; set; }
        public virtual DbSet<tbl_FFM> tbl_FFM { get; set; }
        public virtual DbSet<tbl_Omoor> tbl_Omoor { get; set; }
        public virtual DbSet<tbl_PayehFFM> tbl_PayehFFM { get; set; }
        public virtual DbSet<tbl_PayehFFZ> tbl_PayehFFZ { get; set; }
        public virtual DbSet<tbl_PFT> tbl_PFT { get; set; }
        public virtual DbSet<tbl_PT> tbl_PT { get; set; }
        public virtual DbSet<tbl_QFFM> tbl_QFFM { get; set; }
        public virtual DbSet<tbl_Secsuner> tbl_Secsuner { get; set; }
        public virtual DbSet<tbl_Tablo> tbl_Tablo { get; set; }
        public virtual DbSet<tbl_Trance> tbl_Trance { get; set; }
        public virtual DbSet<Tbl_Derakht_Tajhizat> Tbl_Derakht_Tajhizat { get; set; }
          public DbSet<Form> Forms => Set<Form>();
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>()
    .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasKey(x => new { x.RoleId, x.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId);
            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasQueryFilter(e => !e.IsDeleted);
                entity.ToTable("Tbl_Forms");

                entity.HasKey(e => e.IdForm);

                entity.Property(e => e.IdForm)
                      .HasColumnName("ID_Form")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Onvan)
                      .HasMaxLength(500);

                entity.Property(e => e.Description)
                      .HasMaxLength(500);
            });
            modelBuilder.Entity<tbl_FFM>(entity =>
            {
                entity.Property(e => e.Havaee_FFM)
                      .HasPrecision(18, 3);

                entity.Property(e => e.Zamini_FFM)
                      .HasPrecision(18, 3);

                entity.Property(e => e.Ghati_FFM)
                      .HasPrecision(8, 4);
            });

            modelBuilder.Entity<tbl_Omoor>(entity =>
            {
                entity.Property(e => e.GlobalID)
                      .IsUnicode(false);
            });

            modelBuilder.Entity<tbl_QFFM>(entity =>
            {
                entity.Property(e => e.Havaee_QFFM)
                      .HasPrecision(18, 3);

                entity.Property(e => e.Zamini_QFFM)
                      .HasPrecision(18, 3);
            });
        }
    }
}
