using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public interface IItemOperation
    {
        Task<ApiResponse> AddProductQuantity(ProductQuantityDTO itemDto);
        Task<ApiResponse> GetProductQuantityById(int Id);
        Task<ApiResponse> GetAllProductsQuantity();
        Task<ApiResponse> UpdateProductQuantity(ProductQuantityDTO itemDto);
        Task<ApiResponse> DeleteProductQuantity(int productId);
    }
}
