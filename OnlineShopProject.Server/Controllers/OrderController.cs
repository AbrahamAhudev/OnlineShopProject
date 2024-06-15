using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Repository;
using Org.BouncyCastle.Asn1.X509;

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
        private readonly ICartRepository _CartRepository;
        private readonly ICartItemRepository _CartItemRepository;
        private readonly IOrderItemRepository _OrderItemRepository;

        public OrderController(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            DataContext context,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IOrderItemRepository orderItemRepository)
        {
            _OrderRepository = orderRepository;
            _ProductRepository = productRepository;
            _UserRepository = userRepository;
            _CartRepository = cartRepository;
            _Context = context;
            _CartItemRepository = cartItemRepository;
            _OrderItemRepository = orderItemRepository;

        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Order>))]

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
        [ProducesResponseType(200, Type = typeof(Models.Order))]
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

        [HttpGet("user/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Order>))]
        [ProducesResponseType(400)]

        public IActionResult GetOrdersOfAnUser(int id)
        {

            if (!_UserRepository.UserExists(id))
            {
                return NotFound();
            }

            var orders = _Context.Orders.Where(o => o.UserId == id).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(orders);
        }

        [HttpGet("{id}/price")]
        [ProducesResponseType(200, Type = typeof(Models.Order))]
        [ProducesResponseType(400)]

        public IActionResult GetPriceOfAnOrder(int id)
        {

            if (!_OrderRepository.OrderExists(id))
            {
                return NotFound();
            }

            var order = _Context.Orders.Where(o => o.Id == id).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var OrderItems = _Context.OrderItems.Where(oi => oi.OrderId == order.Id).ToList();

            decimal totalPrice = 0;


            foreach (var orderItem in OrderItems)
            {
                var product = _Context.Products.Where(p => p.Id == orderItem.ProductId).FirstOrDefault();
                if (product != null)
                {
                    totalPrice += product.Price * orderItem.Quantity;
                }
            }

            return Ok(totalPrice);

     
        }

        [HttpGet("{id}/quantity")]
        [ProducesResponseType(200, Type = typeof(Models.Order))]
        [ProducesResponseType(400)]

        public IActionResult GetQuantityOfItemsInAnOrder(int id)
        {

            if (!_OrderRepository.OrderExists(id))
            {
                return NotFound();
            }

            var order = _Context.Orders.Where(o => o.Id == id).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var OrderItems = _Context.OrderItems.Where(oi => oi.OrderId == order.Id).ToList();

            int quantity = 0;


            foreach (var orderItem in OrderItems)
            {
                quantity += orderItem.Quantity;
                
            }

            return Ok(quantity);


        }

        [HttpDelete("{OrderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteOrder(int OrderId)
        {
            if (!_OrderRepository.OrderExists(OrderId))
                return NotFound();

            var OrderToDelete = _OrderRepository.GetOrderById(OrderId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         var OrderItemsToDelete = _OrderItemRepository.GetOrderItemsOfAnOrder(OrderId);

            if(!_OrderItemRepository.DeleteOrderItems(OrderItemsToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the items");
            }

            if (!_OrderRepository.DeleteOrder(OrderToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the order");
            }

            return NoContent();

        }

        [HttpPost("{cartId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateOrder(int cartId, [FromBody] string address)
        {

            if (!_CartRepository.CartExists(cartId))
            {
                return NotFound();
            }

            Cart Cart = _Context.Carts.FirstOrDefault(c => c.Id == cartId);


            if (Cart == null)
            {
                return NotFound("Cart not found");
            }

            var CartItems = _Context.CartItems.Where(ci => ci.CartId == cartId).ToList();

            if(CartItems.Count == 0)
            {
                return NotFound("cart has no items");
            }

             var NewOrder = new Models.Order
             { 
                 UserId = Cart.UserId,
                 Address = address
             };


            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    _Context.Orders.Add(NewOrder);

                    _Context.SaveChanges();

                    foreach (var cartItem in CartItems)
                    {
                        var orderItem = new OrderItem
                        {
                            OrderId = NewOrder.Id,
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity
                        };

                        _Context.Add(orderItem);
                        _Context.SaveChanges();

                    }

                    List<CartItem> CartItemsToDelete = _Context.CartItems.Where(ci => ci.CartId == Cart.Id).ToList();

                    _CartItemRepository.DeleteCartItems(CartItemsToDelete);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }
            }


            

            return Ok(NewOrder);
        }



    }
}
