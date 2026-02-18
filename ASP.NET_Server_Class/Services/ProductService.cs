using ASP.NET_Server_Class.Models;

namespace ASP.NET_Server_Class.Services
{
    public class ProductService
    {
        public readonly UsersDbContext _context;

        public ProductService(UsersDbContext context)
        {
            _context = context;
        }
        public List<Product> GetAll() => _context.Products.ToList();

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        //public List<Product> GetByIds(int[]? ids) {
        //    List<Product> responce = new List<Product>();
        //    foreach (int id in ids)
        //    {
        //        responce.Add(_context.Products.Where(i => i.Id == id).FirstOrDefault());
        //    }
        //    return responce;
        //}
        public List<Product> GetByIds(ProductsInOrder[]? ids)
        {
            List<Product> responce = new List<Product>();
            foreach (ProductsInOrder id in ids)
            {
                responce.Add(_context.Products.Where(i => i.Id == id.ProductId).FirstOrDefault());
            }
            return responce;
        }
    }

    public class OrderService
    {
        public readonly UsersDbContext _context;

        public OrderService(UsersDbContext context)
        {
            _context = context;
        }
        public List<Order> GetAll() => _context.Orders.ToList();

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
    public class ProductsInOrdersService
    {
        public readonly UsersDbContext _context;

        public ProductsInOrdersService(UsersDbContext context)
        {
            _context = context;
        }
        public List<ProductsInOrder> GetAll() => _context.ProductsInOrders.ToList();

        public void Add(ProductsInOrder productsInOrder)
        {
            _context.ProductsInOrders.Add(productsInOrder);
            _context.SaveChanges();
        }
    }

}

