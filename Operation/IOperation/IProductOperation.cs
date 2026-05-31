using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public interface IProductOperation
    {
        Task<ApiResponse> AddProduct(AddProductDTO productDTO);
        Task<ApiResponse> UpdateProduct(UpdateProductDTO updateDto);
        Task<ApiResponse> GetProductById(int ProductId);
        Task<ApiResponse> GetAllProductList(int page, int pageSize);
        Task<ApiResponse> RemoveProductById(int ProductId);
        Task<ApiResponse> GetProductsByName(string? name, int page, int pageSize);
    }
}
