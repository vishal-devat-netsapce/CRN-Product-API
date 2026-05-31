using DTOModel;
using Microsoft.Extensions.Logging;
using Repository;

namespace Operation
{
    public class ItemOperation : IItemOperation
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemOperation> _logger;

        public ItemOperation(
            IItemRepository itemRepository,
            ILogger<ItemOperation> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<ApiResponse> AddProductQuantity(ProductQuantityDTO itemDto)
        {
            try
            {
                return await _itemRepository.AddProductQuantity(itemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding product quantity.");
                throw;
            }
        }

        public async Task<ApiResponse> UpdateProductQuantity(ProductQuantityDTO itemDto)
        {
            try
            {
                return await _itemRepository.UpdateProductQuantity(itemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product quantity.");
                throw;
            }
        }

        public async Task<ApiResponse> GetProductQuantityById(int id)
        {
            try
            {
                return await _itemRepository.GetProductQuantityById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while fetching product quantity. Id: {Id}",
                    id);
                throw;
            }
        }

        public async Task<ApiResponse> GetAllProductsQuantity()
        {
            try
            {
                return await _itemRepository.GetAllProductsQuantity();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while fetching all product quantities.");
                throw;
            }
        }

        public async Task<ApiResponse> DeleteProductQuantity(int productId)
        {
            try
            {
                return await _itemRepository.DeleteProductQuantity(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while deleting product quantity. ProductId: {ProductId}",
                    productId);
                throw;
            }
        }
    }
}