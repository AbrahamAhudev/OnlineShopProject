using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Repository;

namespace OnlineShopProject.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CartItemController : Controller
    {

        private readonly DataContext _DataContext;
        private readonly ICartItemRepository _CartItemRepository;

        public CartItemController(DataContext context, ICartItemRepository cartitemrepository)
        {
            _DataContext = context;
            _CartItemRepository = cartitemrepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CartItem>))]

        public IActionResult GetCartItems()
        {
            var CartItems = _CartItemRepository.GetCartItems();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(CartItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CartItem))]
        [ProducesResponseType(400)]

        public IActionResult GetCartItem(int id)
        {
            if (!_CartItemRepository.CartItemExists(id))
            {
                return NotFound();
            }

            var CartItem = _CartItemRepository.GetCartItem(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(CartItem);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCartItem(int id)
        {
            if (!_CartItemRepository.CartItemExists(id))
                return NotFound();

            CartItem CartItemToDelete = _CartItemRepository.GetCartItem(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(CartItemToDelete.Quantity > 1)
            {

                int quantity = CartItemToDelete.Quantity;

                CartItemToDelete.Quantity = quantity - 1;

                _DataContext.SaveChanges();

                return NoContent();
            }
            
            if (!_CartItemRepository.DeleteCartItem(CartItemToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the cart");
            }

            return NoContent();

        }
    }
}
