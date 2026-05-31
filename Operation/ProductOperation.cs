using DTOModel;
using Microsoft.Extensions.Logging;
using Repository;

namespace Operation
{
    public class ProductOperation : IProductOperation
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductOperation> _logger;

        public ProductOperation(
            IProductRepository productRepository,
            ILogger<ProductOperation> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ApiResponse> AddProduct(AddProductDTO productDTO)
        {
            try
            {
                return await _productRepository.AddProduct(productDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding product.");
                throw;
            }
        }

        public async Task<ApiResponse> UpdateProduct(UpdateProductDTO updateDto)
        {
            try
            {
                return await _productRepository.UpdateProduct(updateDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product.");
                throw;
            }
        }

        public async Task<ApiResponse> GetProductById(int ProductId)
        {
            try
            {
                return await _productRepository.GetProductById(ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,"Error occurred while fetching product. ProductId: {ProductId}",
                    ProductId);
                throw;
            }
        }

        public async Task<ApiResponse> GetAllProductList(int page, int pageSize)
        {
            try
            {
                return await _productRepository.GetAllProductList(page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching product list.");
                throw;
            }
        }

        public async Task<ApiResponse> RemoveProductById(int ProductId)
        {
            try
            {
                return await _productRepository.RemoveProductById(ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while removing product. ProductId: {ProductId}",ProductId);
                throw;
            }
        }

        public async Task<ApiResponse> GetProductsByName(string? name, int page, int pageSize)
        {
            try
            {
                return await _productRepository.GetProductsByName(name, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while searching products. SearchText: {Name}",name);
                throw;
            }
        }
    }
}