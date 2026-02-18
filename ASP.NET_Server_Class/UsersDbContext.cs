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

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        
    }
}
