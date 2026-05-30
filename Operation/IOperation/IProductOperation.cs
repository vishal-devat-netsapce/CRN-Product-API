using DTOModel;
using DTOModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.IOperation
{
    public interface IProductOperation
    {
        Task<ApiResponse> AddProduct(AddProductDTO productDTO);
        Task<ApiResponse> UpdateProduct(UpdateProductDTO updateDto);

        Task<ApiResponse> GetProductById(int ProductId);
        Task<ApiResponse> GetAllProductList();
    }
}
