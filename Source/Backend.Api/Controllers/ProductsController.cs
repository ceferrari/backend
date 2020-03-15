using System;
using System.Threading.Tasks;
using Backend.Application.Services;
using Backend.Domain.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")] 
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute]Guid id, [FromQuery]string sku)
        {
            if (id != Guid.Empty && sku == null)
            {
                return await _productService.GetById(id);
            }

            if (id == Guid.Empty && sku != null)
            {
                return await _productService.GetBySku(sku);
            }

            return await _productService.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductViewModel product)
        {
            return await _productService.Create(product);
        }
    }
}
