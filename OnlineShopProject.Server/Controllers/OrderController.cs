using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Repository;

namespace OnlineShopProject.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly IOrderRepository _OrderRepository;
        private readonly IProductRepository _ProductRepository;
        private readonly IUserRepository _UserRepository;
        private readonly DataContext _Context;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, DataContext context)
        {
            _OrderRepository = orderRepository;
            _ProductRepository = productRepository;
            _UserRepository = userRepository;
            _Context = context;

        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]

        public IActionResult GetOrders()
        {
            var Orders = _OrderRepository.GetOrders();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]

        public IActionResult GetOrder(int id)
        {

            if(!_OrderRepository.OrderExists(id))
            {
                return NotFound();
            }

            var order = _OrderRepository.GetOrderById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(order);
        }


        [HttpDelete("{OrderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteOrder(int OrderId)
        {
            if (!_OrderRepository.OrderExists(OrderId))
                return NotFound();

            Order OrderToDelete = _OrderRepository.GetOrderById(OrderId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_OrderRepository.DeleteOrder(OrderToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the order");
            }

            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateOrder([FromBody] Order NewOrder)
        {
            if (NewOrder == null)
            {
                return BadRequest();
            }

            if (!_UserRepository.UserExists(NewOrder.UserId))
            {
                return NotFound();
            }

            if (!_ProductRepository.ProductExists(NewOrder.ProductId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_OrderRepository.CreateOrder(NewOrder))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("order successfully created");
        }


        [HttpGet("Product/{Productid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(400)]

        public IActionResult GetOrdersOfAProduct(int Productid)
        {

            if (!_ProductRepository.ProductExists(Productid))
            {
                return NotFound();
            }

            var orders = _Context.Orders.Where(o => o.ProductId == Productid).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(orders);
        }

    }
}
