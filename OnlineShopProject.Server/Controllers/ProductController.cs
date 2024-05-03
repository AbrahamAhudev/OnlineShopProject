using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
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


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateProduct([FromBody] Product NewProduct)
        {
            if (NewProduct == null)
            {
                return BadRequest();
            }

            var product = _ProductRepository.GetProducts()
                .Where(u => u.Name.ToLower() == NewProduct.Name.ToLower()).FirstOrDefault();

            if (product is not null)
            {
                ModelState.AddModelError("", "product already exists");
                return StatusCode(422, ModelState);
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

        public IActionResult UpdateProduct(int ProductId, [FromBody] Product UpdatedProduct)
        {
            if (UpdatedProduct is null)
                return BadRequest(ModelState);

            if (ProductId != UpdatedProduct.Id)
                return BadRequest(ModelState);

            if (!_ProductRepository.ProductExists(ProductId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_ProductRepository.DeleteProduct(ProductToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the product");
            }

            return NoContent();

        }

    }
}
