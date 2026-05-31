using DTOModel;
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

        public async Task<ApiResponse> GetProductsByName(string? name,int page,int pageSize)
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 10;

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.ProductName.Contains(name));
            }

            var totalRecords = await query.CountAsync();

            if (totalRecords == 0)
            {
                return new ApiResponse("404",false,null,"No products found.");
            }

            var products = await query
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResponse("200",true, products,"Products retrieved successfully.");
        }

        public async Task<ApiResponse> RemoveProductById(int ProductId)
        {
            var data = await _context.Products.Where(x => x.Id == ProductId).FirstOrDefaultAsync();

            if (data == null)
            {
                return new ApiResponse("404", false, null, "Product not found on the be half of productId");
            }

             _context.Products.Remove(data);
            await _context.SaveChangesAsync();

            return new ApiResponse("200", true, null, "Product successfully removed.");
        }

        public async Task<ApiResponse> GetAllProductList(int page,int pageSize)
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 10;

            var totalRecords = await _context.Products.CountAsync();

            var products = await _context.Products.AsNoTracking().OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();

            return new ApiResponse("200", true,products,"Products retrieved successfully.");
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
            

                bool isExist = await _context.Products.AnyAsync(x => x.ProductName == updateProductDTO.ProductName);
                if (isExist == true)
                {
                    return new ApiResponse("400",false,null, "Product name already exists."); 
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
          
        }

        public async Task<ApiResponse> AddProduct(AddProductDTO dto)
        {
          
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
          
        }
    }

}
