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
    public class ItemRepository : IItemRepository
    {
        private readonly ProductsContext _productsContext;
        public ItemRepository(ProductsContext productsContext)
        {
            _productsContext = productsContext;
        }

        public async Task<ApiResponse> DeleteProductQuantity(int productId)
        {
            var item = await _productsContext.Items.FirstOrDefaultAsync(x=>x.ProductId==productId);

            if (item == null)
            {
                return new ApiResponse("404", false, null,"Product quantity record not found.");
            }

            _productsContext.Items.Remove(item);
            await _productsContext.SaveChangesAsync();

            return new ApiResponse("200", true, null,"Product quantity deleted successfully.");
        }

        public async Task<ApiResponse> UpdateProductQuantity(ProductQuantityDTO itemDto)
        {
            if (itemDto.Quantity <= 0)
            {
                return new ApiResponse("400", false, null,"Quantity must be greater than zero.");
            }

            var item = await _productsContext.Items.FirstOrDefaultAsync(x => x.ProductId == itemDto.ProductId);

            if (item == null)
            {
                return new ApiResponse("404", false, null,"Product quantity record not found.");
            }

            item.Quantity = itemDto.Quantity;
            await _productsContext.SaveChangesAsync();

            return new ApiResponse("200", true, item,"Product quantity updated successfully.");
        }
        public async Task<ApiResponse> AddProductQuantity(ProductQuantityDTO itemDto)
        {
            if (itemDto.Quantity <= 0)
            {
                return new ApiResponse("400", false, null, "Quantity must be greater than zero.");
            }

            var productExists = await _productsContext.Products.AnyAsync(x => x.Id == itemDto.ProductId);

            if (!productExists)
            {
                return new ApiResponse("404", false, null, "Product not found.");
            };

            var ExistingItem = await _productsContext.Items.FirstOrDefaultAsync(x => x.ProductId == itemDto.ProductId);

            if (ExistingItem != null) {
                ExistingItem.Quantity += itemDto.Quantity;
                await _productsContext.SaveChangesAsync();
                return new ApiResponse("200", true, ExistingItem, "Product quantity updated successfully.");
            }

            Item newItem = new Item
            {
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
            };

            await _productsContext.Items.AddAsync(newItem);
            await _productsContext.SaveChangesAsync();

            return new ApiResponse("200", true, newItem, "Product quantity added successfully.");

        }


        public async Task<ApiResponse> GetProductQuantityById(int id)
        {
            bool quanityExists = await _productsContext.Items.AnyAsync(x => x.ProductId == id);

            if (!quanityExists)
            {
                return new ApiResponse("404", false, null, "Product quantity not found.");
            };

            var itmquantity = await ( from item in _productsContext.Items
                join product in _productsContext.Products on item.ProductId equals product.Id

                select new QuantityReponseDTO
                {
                    ProductId = item.Id,
                    ProductName=product.ProductName,
                    Quantity=item.Quantity,
                }
             ).FirstOrDefaultAsync();

            return new ApiResponse("200", true, itmquantity, "Product quantity found successfully.");

        }

        public async Task<ApiResponse> GetAllProductsQuantity()
        {

            var productItem=await (from item in _productsContext.Items
                   join product in _productsContext.Products on item.ProductId equals product.Id

                   select new QuantityReponseDTO
                   {
                       ProductId = item.Id,
                       ProductName=product.ProductName,
                       Quantity = item.Quantity
                   }

                   ).ToListAsync();

            if (productItem.Count()==0)
            {
                return new ApiResponse("404", false, null, "All products quantity not found.");
            }
            ;

            return new ApiResponse("200", true, productItem, "All products quantity found successfully.");

        }

    }
}
