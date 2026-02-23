using Microsoft.EntityFrameworkCore;
using ASP.NET_Server_Class.Models;

namespace ASP.NET_Server_Class
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductsInOrder> ProductsInOrders { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorsSpecializations> DoctorsSpecializations { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Department> Departments { get; set; }


        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        
    }
}
