using DTOModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operation;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductOperation _productOperation;
        public ProductController(IProductOperation productOperation)
        {
             _productOperation = productOperation;
        }

        [HttpPost]
        public async Task<ApiResponse> AddProduct([FromForm]AddProductDTO productDTO)
        {
                return await _productOperation.AddProduct(productDTO);
        }

        [HttpPut]
        public async Task<ApiResponse> UpdateProduct([FromForm] UpdateProductDTO productDTO)
        {
            return await _productOperation.UpdateProduct(productDTO);
        }

        [HttpGet("{ProductId:int}")]
        public async Task<ApiResponse> RetrieveProductById(int ProductId)
        {     
            return await _productOperation.GetProductById(ProductId);          
        }

        [HttpGet]
        public async Task<ApiResponse> RetrieveAllProduct( int page = 1,int pageSize = 10)
        {
            return await _productOperation.GetAllProductList(page, pageSize);
        }

        [HttpGet("search")]
        public async Task<ApiResponse> GetProductsByName(string? name, int page = 1, int pageSize = 10)
        {
            //return await _productOperation.GetProductsByName(name, page, pageSize);
            throw new Exception("Testing  error");
        }

        [HttpDelete]
        public async Task<ApiResponse> RemoveProductById(int ProductId)
        {
            throw new Exception("Testing Custom Middleware");
            //return await _productOperation.RemoveProductById(ProductId);
        }
    }
}
