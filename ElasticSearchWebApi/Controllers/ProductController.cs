using ElasticSearchWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ElasticSearchWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService,
            ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
       
        [HttpGet]
        [Route("/[controller]/[action]")]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetAll();
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public Product GetProductById(int id)
        {
            return _productService.GetById(id);
        }

        [HttpPost]
        [Route("api/AddProduct")]
        public void AddProduct([FromBody] Product product)
        {
            _productService.Add(product);
        }

        [HttpPost]
        [Route("api/UpdateProduct")]
        public void UpdateProduct([FromBody] Product product)
        {
            _productService.Update(product);
        }

        [HttpDelete]
        [Route("api/DeleteByProductId/{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }

        
    }
}
