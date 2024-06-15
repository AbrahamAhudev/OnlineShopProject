using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.DTOs;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Repository;

namespace OnlineShopProject.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CartController : Controller
    {
        private readonly DataContext _DataContext;
        private readonly ICartRepository _CartRepository;
        private readonly IUserRepository _UserRepository;

        public CartController(DataContext context, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _DataContext = context;
            _CartRepository = cartRepository;
            _UserRepository = userRepository;

        }

        [HttpPost("{userId}/add")]
        public IActionResult AddToCart(int userId, [FromBody] int productId)
        {
            var user = _DataContext.Users.Find(userId);

            if(user == null)
            {
                return NotFound("user not found");

            }

            Cart cart = _DataContext.Carts
                         .Where(c => c.UserId == userId).FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart { UserId = userId };

                _DataContext.Carts.Add(cart);
                
               

                _DataContext.SaveChanges();

            }

            var product = _DataContext.Products.Find(productId);

            if(product == null)
            {
                return NotFound("product not found");
            }

            CartItem cartItem = _DataContext.CartItems
                 .Where(ci => ci.ProductId == productId && ci.CartId == cart.Id).FirstOrDefault();



            if (cartItem == null)
            {
              
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                
                
                _DataContext.CartItems.Add(cartItem);

                _DataContext.SaveChanges();
            }
            else
            {
                cartItem.Quantity += 1;
            }

            user.CartId = cart.Id;

            _DataContext.SaveChanges();

            return Ok(cart);
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Cart>))]

        public IActionResult GetCarts()
        {
            var Carts = _CartRepository.GetCarts();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Carts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Cart))]
        [ProducesResponseType(400)]

        public IActionResult GetCart(int id)
        {
            if (!_CartRepository.CartExists(id))
            {
                return NotFound();
            }

            var Cart = _CartRepository.GetCart(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Cart);
        }


        [HttpDelete("{Cartid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCart(int Cartid)
        {
            if (!_CartRepository.CartExists(Cartid))
                return NotFound();

            Cart CartToDelete = _CartRepository.GetCart(Cartid);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CartUser = _UserRepository.GetUserById(CartToDelete.UserId);

            CartUser.CartId = null;

           

            if (!_CartRepository.DeleteCart(CartToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the cart");
            }

            _DataContext.SaveChanges();

            return NoContent();

        }


        [HttpGet("{id}/products")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CartItemDTO>))]
        [ProducesResponseType(400)]

        public IActionResult GetProductsOfACart(int id)
        {

            if (!_CartRepository.CartExists(id))
            {
                return NotFound();
            }

            var CartItems = _DataContext.CartItems
                .Where(ci => ci.CartId == id)
                .ToList();

            if (!CartItems.Any())
            {
                return Ok(new List<CartItemDTO>());
            }

            var productIds = CartItems.Select(ci => ci.ProductId).ToList();

            var products = _DataContext.Products
               .Where(p => productIds.Contains(p.Id))
               .ToDictionary(p => p.Id);


            var cartItemDtos = CartItems.Select(ci => new CartItemDTO
            {
                CartItemId = ci.Id,
                ProductId = ci.ProductId,
                ProductName = products[ci.ProductId].Name,
                ProductDescription = products[ci.ProductId].Description,
                ProductPrice = products[ci.ProductId].Price,
                Quantity = ci.Quantity,
                Image = products[ci.ProductId].Image,

            }).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cartItemDtos);
        }

    }
}
