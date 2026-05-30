using Operation.IOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using System.Threading.Tasks;
using DTOModels.Response;
using DTOModel;

namespace Operation
{
    public class ProductOperation : IProductOperation
    {
        private readonly IProductRepository _productRepository;
        public ProductOperation(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse> AddProduct(AddProductDTO productDTO) 
        {
            try
            {
                return await _productRepository.AddProduct(productDTO);

                //if (data == null)
                //{
                //    return new ApiResponse("400",false,null,"Failed to add product.");
                //}
                //else { 
                //    return new ApiResponse("200", true, data, "Product added successfully.");
                //}
            }
            catch (Exception ex)
            {
                //return await _productRepository.AddProduct(productDTO);
                return new ApiResponse("500", false, null, "An error occurred while adding the product." + ex.InnerException?.Message ?? ex.Message);
            }

        }

        public async Task<ApiResponse> UpdateProduct(UpdateProductDTO updateDto)
        {
            try
            {
                return await _productRepository.UpdateProduct(updateDto);

                //if (data == null)
                //{
                //    return new ApiResponse("400",false,null,"Failed to add product.");
                //}
                //else { 
                //    return new ApiResponse("200", true, data, "Product added successfully.");
                //}
            }
            catch (Exception ex)
            {
                return new ApiResponse("500", false, null,"An error occurred during updating the product." +ex.Message);
            };
        }

      

        public async Task<ApiResponse> GetProductById(int ProductId)
        {
            try
            {
                return await _productRepository.GetProductById(ProductId);

                //if (data == null)
                //{
                //    return new ApiResponse("400",false,null,"Failed to add product.");
                //}
                //else { 
                //    return new ApiResponse("200", true, data, "Product added successfully.");
                //}
            }
            catch (Exception ex)
            {
                return new ApiResponse("500", false, null, "An error occurred while fetching the product by productId." + ex.InnerException?.Message ?? ex.Message);
            }
            ;
        }

        public async Task<ApiResponse> GetAllProductList()
        {
            try
            {
                return await _productRepository.GetProductLisr();

                //if (data == null)
                //{
                //    return new ApiResponse("400",false,null,"Failed to add product.");
                //}
                //else { 
                //    return new ApiResponse("200", true, data, "Product added successfully.");
                //}
            }
            catch (Exception ex)
            {
                return new ApiResponse("500", false, null, "An error occurred while fetching the product list." + ex.InnerException?.Message ?? ex.Message);
            }
            ;
        }

    }
    
}
