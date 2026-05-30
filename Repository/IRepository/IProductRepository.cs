using DTOModel;
using DTOModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        Task<ApiResponse> AddProduct(AddProductDTO dto);
        Task<ApiResponse> UpdateProduct(UpdateProductDTO updateProductDTO);
        Task<ApiResponse> GetProductById(int ProductId);
        Task<ApiResponse> GetProductLisr();
    }
}
