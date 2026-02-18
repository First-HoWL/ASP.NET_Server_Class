using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Server_Class.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
    }
    public class ProductsInOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }

    public class OrderWithProducts : Order
    {
        public List<Product> Products { get; set; }
        public OrderWithProducts(Order order, List<Product> products)
        {
            Id = order.Id;
            CustomerId = order.CustomerId;
            TotalPrice = order.TotalPrice;
            Products = products;
        }
    }
}
