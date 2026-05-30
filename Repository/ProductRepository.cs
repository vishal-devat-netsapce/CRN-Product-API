using DTOModel;
using DTOModels.Response;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private  readonly ProductsContext _context;
        public ProductRepository(ProductsContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetProductLisr()
        {
            var data = await _context.Products.ToListAsync();

            if (data == null)
            {
                return new ApiResponse("404", false, null, "Product list not found.");
            }

            return new ApiResponse("200", true, data, "Product list successfully found.");
        }

        public async Task<ApiResponse> GetProductById(int ProductId)
        {
             var data = await _context.Products.Where(x => x.Id == ProductId).FirstOrDefaultAsync();

            if (data == null) {
                return new ApiResponse("404",false,null,"Product not found on the be half of productId");
            }

            
              return new ApiResponse("200", true,data ,"Product successfully found.");
        }

        public async Task<ApiResponse> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            //try
            //{

                bool isExist = await _context.Products.AnyAsync(x => x.ProductName == updateProductDTO.ProductName);
                if (isExist == true)
                {
                    return new ApiResponse("400",false,null, "Product name already exists."); ;
                }

                var product = await _context.Products.Where(x => x.Id == updateProductDTO.Id).FirstOrDefaultAsync();

                if (product == null) {
                    return new ApiResponse("404", false, null, "Product not found.");
                }

                product.ProductName= updateProductDTO.ProductName;
                product.ModifiedBy= updateProductDTO.ModifiedBy;
                product.ModifiedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            EditProductResponseDTO result =  new EditProductResponseDTO
                { 
                    Id= product.Id,
                    ProductName = product.ProductName,
                    ModifiedBy= product.ModifiedBy,
                    ModifiedOn = product.ModifiedOn
                };

                return new ApiResponse("200",true, result, "Product updated successfully.");
            //}
            //catch (Exception ex)
            //{
            //    return new ApiResponse("500",false, null, "An error occurred during updating product."+ex.Message);
            //};
        }

        public async Task<ApiResponse> AddProduct(AddProductDTO dto)
        {
            //try
            //{
                bool isExist = await _context.Products
                    .AnyAsync(x => x.ProductName == dto.ProductName);

                if (isExist)
                {
                    return new ApiResponse("400", false, null, "Product name already exists.");
                }

                var product = new Product
                {
                    ProductName = dto.ProductName,
                    CreatedBy = dto.CreatedBy,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = null,
                    ModifiedOn = null
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                var result = new GetProduct
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    CreatedBy = product.CreatedBy,
                    CreatedOn = product.CreatedOn
                };

                return new ApiResponse("200", true, result, "Product inserted successfully.");
            //}
            //catch (Exception ex)
            //{
            //    return new ApiResponse("500", false, null, "An error occcurred during inserting the product"+ex.Message);
            //}
        }
    }

}
