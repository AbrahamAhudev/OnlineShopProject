using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.DTOs;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Repository;

namespace OnlineShopProject.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : Controller
    {

        private readonly IProductRepository _ProductRepository;
        private readonly ICartItemRepository _CartItemRepository;
        private readonly DataContext _DataContext;
        private readonly IOrderItemRepository _OrderItemRepository;

        public ProductController(IProductRepository productRepository, ICartItemRepository cartItemRepository, DataContext context, IOrderItemRepository orderItemRepository)
        {
            _ProductRepository = productRepository;
            _CartItemRepository = cartItemRepository;
            _DataContext = context;
            _OrderItemRepository = orderItemRepository;

        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]

        public IActionResult GetProducts()
        {
            var Products = _ProductRepository.GetProducts();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Products);
        }

        [HttpGet("user/{UserId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]

        public IActionResult GetProductsOfUser(int UserId)
        {

            var Products = _DataContext.Products.Where(p => p.UserId == UserId).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]

        public IActionResult GetProduct(int id)
        {
            if (!_ProductRepository.ProductExists(id))
            {
                return NotFound();
            }

            var Product = _ProductRepository.GetProductById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Product);
        }

        [HttpGet("search/{searchstring}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]

        public IActionResult SearchProduct(string searchstring)
        {

            var Products = _ProductRepository.SearchProducts(searchstring);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Products);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateProduct([FromForm] ProductDTO NewProductDTO)
        {
            if (NewProductDTO == null)
            {
                return BadRequest();
            }

            var product = _ProductRepository.GetProducts()
                .Where(u => u.Name.ToLower() == NewProductDTO.Name.ToLower()).FirstOrDefault();

            if (product is not null)
            {
                ModelState.AddModelError("", "product already exists");
                return StatusCode(422, ModelState);
            }

            string filename = null;

            var NewProduct = new Product
            {
                Name = NewProductDTO.Name,
                Description = NewProductDTO.Description,
                Price = NewProductDTO.Price,
                UserId = NewProductDTO.UserId
            };

            if (NewProductDTO.Image != null)
            {
                try
                {
                    filename = SaveImage(NewProductDTO.Image);
                    NewProduct.Image = filename;
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return BadRequest(ModelState);
                }
            }



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_ProductRepository.CreateProduct(NewProduct))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("product successfully created");
        }


    [HttpPut("{ProductId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateProduct(int ProductId, [FromForm] ProductDTO UpdatedProductDTO)
        {
            if (UpdatedProductDTO is null)
                return BadRequest(ModelState);

            

            if (!_ProductRepository.ProductExists(ProductId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UpdatedProduct = new Product
            {
                Id = ProductId,
                Name = UpdatedProductDTO.Name,
                Description = UpdatedProductDTO.Description,
                Price = UpdatedProductDTO.Price,
                UserId = UpdatedProductDTO.UserId
            };

            string filename = null;

            if (UpdatedProductDTO.Image != null)
            {
                try
                {
                    filename = SaveImage(UpdatedProductDTO.Image);
                    UpdatedProduct.Image = filename;
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return BadRequest(ModelState);
                }
            }

            if (!_ProductRepository.UpdateProduct(UpdatedProduct))
            {
                ModelState.AddModelError("", "Something went wrong when saving");

                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{ProductId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteProduct(int ProductId)
        {
            if (!_ProductRepository.ProductExists(ProductId))
                return NotFound();

            Product ProductToDelete = _ProductRepository.GetProductById(ProductId);

            if(!string.IsNullOrEmpty(ProductToDelete.Image))
            {
               
                string relativePath = Path.Combine("..", "onlineshopproject.client", "src", "assets", "images", "products");
         
                string imagePath = Path.Combine(relativePath, ProductToDelete.Image);
     
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CartItemsToDelete = _DataContext.CartItems.Where(ci => ci.ProductId == ProductId).ToList();



            if (CartItemsToDelete.Any())
            {
                if (!_CartItemRepository.DeleteCartItems(CartItemsToDelete))
                {
                    ModelState.AddModelError("", "something went wrong deleting the product");
                }
            }

            var OrderItemsToDelete = _DataContext.OrderItems.Where(oi => oi.ProductId == ProductId).ToList();

            if (!_OrderItemRepository.DeleteOrderItems(OrderItemsToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the order items");
            }

            if (!_ProductRepository.DeleteProduct(ProductToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the product");
            }

            return NoContent();

        }


        private string SaveImage(IFormFile image)
        {


            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var permittedMimeTypes = new[] { "image/jpeg", "image/png" };

            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if(string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension)){

                throw new InvalidOperationException("Unsupported file extension");
            }

            if (!permittedMimeTypes.Contains(image.ContentType.ToLowerInvariant()))
            {

            }

            string relativePath = Path.Combine("..", "onlineshopproject.client", "src", "assets", "images", "products");

            string uploadsFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), relativePath));

            string uniqueFileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return uniqueFileName;
        }



    }
}
