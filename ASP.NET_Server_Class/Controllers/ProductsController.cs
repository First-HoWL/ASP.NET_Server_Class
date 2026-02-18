using ASP.NET_Server_Class.Services;
using ASP.NET_Server_Class.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;
        private readonly OrderService _orderService;
        private readonly ProductsInOrdersService _productsInOrdersService;
        public ProductsController(ProductService ProductService, OrderService orderService, ProductsInOrdersService productsInOrdersService, UserService userService)
        {
            _productService = ProductService;
            _orderService = orderService;
            _productsInOrdersService = productsInOrdersService;
            _userService = userService;
        }
        [HttpGet("products")]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(_productService.GetAll());
        }
        [HttpGet("orders")]
        public ActionResult<List<Order>> GetOrders()
        {
            return Ok(_orderService.GetAll());
        }
        [HttpGet("productsInOrder/{id}")]
        public ActionResult<OrderWithProducts> GetproductsInOrder(int id)
        {
            if (_orderService.GetAll().Where(i => i.Id == id) == null)
            {
                return BadRequest();
            }

            return Ok(new OrderWithProducts(
                _orderService.GetAll().Where(i => i.Id == id).FirstOrDefault(),
                _productService.GetByIds(
                    _productsInOrdersService.GetAll().Where(i => i.OrderId == id).ToArray()
                    )
                )
            );
        }
        [HttpPost("AddProduct")]
        public ActionResult AddProduct([FromBody] Product product)
        {
            _productService.Add(product);
            return Ok();
        }
        [HttpPost("AddOrder")]
        public ActionResult AddOrder([FromBody] Order order)
        {
            if (_userService.GetAll().Where(i => i.Id == order.CustomerId) == null)
            {
                return BadRequest();
            }

            _orderService.Add(order);
            return Ok();
        }
        [HttpPost("AddProductToOrder")]
        public ActionResult AddProductToOrder([FromBody] ProductsInOrder productsInOrder)
        {
            if (_productService.GetAll().Where(i => i.Id == productsInOrder.ProductId).FirstOrDefault() != null && _orderService.GetAll().Where(i => i.Id == productsInOrder.OrderId).FirstOrDefault() != null) { 
                _productsInOrdersService.Add(productsInOrder);
                _orderService.GetAll().Where(i => i.Id == productsInOrder.OrderId).FirstOrDefault().TotalPrice += _productService.GetAll().Where(i => i.Id == productsInOrder.ProductId).FirstOrDefault().Price;
                _productService.GetAll().Where(i => i.Id == productsInOrder.ProductId).FirstOrDefault().Stock -= 1;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
