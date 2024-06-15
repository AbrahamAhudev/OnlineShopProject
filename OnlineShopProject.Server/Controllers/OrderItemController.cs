using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
     
        private readonly DataContext _DataContext;
        private readonly IOrderItemRepository _OrderItemRepository;
        private readonly IOrderRepository _OrderRepository;

        public OrderItemController(DataContext context, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository)
        { 
            _DataContext = context;
            _OrderItemRepository = orderItemRepository;
            _OrderRepository = orderRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderItem>))]

        public IActionResult GetOrderItems()
        {
            var OrderItems = _OrderItemRepository.GetOrderItems();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(OrderItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(OrderItem))]
        [ProducesResponseType(400)]

        public IActionResult GetOrderItem(int id)
        {

            if (!_OrderItemRepository.OrderItemExists(id))
            {
                return NotFound();
            }

            var orderitem = _OrderItemRepository.GetOrderItemById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(orderitem);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteOrderItem(int Id)
        {
            if (!_OrderItemRepository.OrderItemExists(Id))
                return NotFound();

            OrderItem OrderItemToDelete = _OrderItemRepository.GetOrderItemById(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_OrderItemRepository.DeleteOrderItem(OrderItemToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the order item");
            }

            return NoContent();

        }
    }
}
