using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.V1
{
     // used for versioning

     [ApiVersion("1.0")]
     public class CatalogController : BaseController
     {
         private readonly IProductRepository _repository;
         private readonly ILogger<CatalogController> _logger;

         public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
         {
             _repository = repository ?? throw new ArgumentNullException(nameof(repository));
             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         }

         [HttpGet]
         public async Task<string> Index()
         {
             return "this is a test";
         }

      
         [HttpGet("GetProducts")]
         [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
         public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
         {
             var products = await _repository.GetPtoducts();
             return Ok(products);
         }

         [HttpGet("[action]/{id:length(24)}")]
         [ProducesResponseType((int) HttpStatusCode.NotFound)]
         [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
         public async Task<ActionResult<Product>> GetProductById(string id)
         {
             var product = await _repository.GetProductById(id);
             if (product == null)
             {
                 _logger.LogError($"Product with id : {id} , NotFound.");
                 return NotFound();
             }

             return Ok(product);
         }

         [HttpGet("[action]/{id:length(24)}", Name = "GetProductsByCategory")]
         [ProducesResponseType((int) HttpStatusCode.NotFound)]
         [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
         public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
         {
             var products = await _repository.GetProductByCategory(category);
             return Ok(products);
         }

         [HttpPost("[action]")]
         [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
         public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
         {
             await _repository.CreateProduct(product);
             return CreatedAtRoute("GetProduct", new {id = product.Id}, product); // it will back to this product
         }

         [HttpPut("[action]")]
         [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
         public async Task<IActionResult> UpdateProduct([FromBody] Product product)
         {
             return Ok(await _repository.UpdateProduct(product));
         }

         [HttpDelete("[action]/{id:length(24)}", Name = "DeleteProduct")]
         [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
         public async Task<IActionResult> DeleteProductById(string Id)
         {
             return Ok(await _repository.DeleteProduct(Id));

         }
     }
}
