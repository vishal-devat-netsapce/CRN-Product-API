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
    public class ItemController : ControllerBase
    {
        private readonly IItemOperation _itemOperation;
        public ItemController(IItemOperation itemOperation) 
        {
            _itemOperation = itemOperation;
        }

        [HttpPost]
        public async Task<ApiResponse> AddProductItem([FromForm] ProductQuantityDTO itemDto)
        {
            return await _itemOperation.AddProductQuantity(itemDto);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllProductsQuantity()
        {
            return await _itemOperation.GetAllProductsQuantity();
        }


        [HttpGet("{ProductId:int}")]
        public async Task<ApiResponse> GetProductQuantityById(int ProductId)
        {
            return await _itemOperation.GetProductQuantityById(ProductId);
        }


        [HttpPut]
        public async Task<ApiResponse> UpdateProductQuantity([FromForm] ProductQuantityDTO itemDto)
        {
            return await _itemOperation.UpdateProductQuantity(itemDto);
        }

        [HttpDelete("{ProductId:int}")]
        public async Task<ApiResponse> DeleteProductQuantity(int ProductId)
        {
            return await _itemOperation.DeleteProductQuantity(ProductId);
        }
    }

}
