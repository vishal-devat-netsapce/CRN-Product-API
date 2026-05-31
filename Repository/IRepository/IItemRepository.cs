using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IItemRepository
    {
        Task<ApiResponse> AddProductQuantity(ProductQuantityDTO itemDto);
        Task<ApiResponse> GetProductQuantityById(int id);
        Task<ApiResponse> GetAllProductsQuantity();
        Task<ApiResponse> DeleteProductQuantity(int productId);
        Task<ApiResponse> UpdateProductQuantity(ProductQuantityDTO itemDto);
    }
}
