using DTOModel;
using DTOModel;
using DTOModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operation;
using Operation.IOperation;
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
        //[AllowAnonymous]
     
        public async Task<ApiResponse> AddProduct([FromForm]AddProductDTO productDTO)
        {
           
                return await _productOperation.AddProduct(productDTO);
          
        }

        [HttpGet("{ProductId:int}")]
        //[AllowAnonymous]
        public async Task<ApiResponse> RetrieveProductById(int ProductId)
        {
            //    try
            //    {
            return await _productOperation.GetProductById(ProductId);
            //}
            //catch (Exception ex)
            //{
            //    return new ApiResponse("500", false, null, "An error occurred during update the product." + ex.Message);

            //}
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResponse> RetrieveAllProduct()
        {
            //    try
            //    {
            return await _productOperation.GetAllProductList();
            //}
            //catch (Exception ex)
            //{
            //    return new ApiResponse("500", false, null, "An error occurred during update the product." + ex.Message);

            //}
        }


        [HttpPut]
        //[AllowAnonymous]
        public async Task<ApiResponse> UpdateProduct([FromForm] UpdateProductDTO productDTO)
        {
            //    try
            //    {
            return await _productOperation.UpdateProduct(productDTO);
            //}
            //catch (Exception ex)
            //{
            //    return new ApiResponse("500", false, null, "An error occurred during update the product." + ex.Message);

            //}
        }

    }
}
