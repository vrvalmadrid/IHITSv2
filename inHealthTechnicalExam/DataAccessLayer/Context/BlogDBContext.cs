using inHealthTechnicalExam.DataAccessLayer.Models;
using inHealthTechnicalExam.Utilities;
using Microsoft.EntityFrameworkCore;

namespace inHealthTechnicalExam.DataAccessLayer.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role() { ID = 1, RoleCode = Constant.ADMIN, RoleType = "Administrator", },
                new Role() { ID = 2, RoleCode = Constant.STANDARD, RoleType = "Standard" });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
           // optionsBuilder.UseSqlServer(@"Server=LAPTOP-VFPDU88O\SQLEXPRESS;Database=BlogDB;Trusted_Connection=True;");
        //}
    }
}
