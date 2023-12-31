using Examenes.Data;
using Examenes.Models;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Examenes.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace Examenes.Services
{

    public class ProductService : IProductService
    {
        private readonly YaPedidosContext _context;

        public ProductService(YaPedidosContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetProductsAsync(string nameFilter)
        {
            var query = _context.Product.AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(x => x.Name.ToLower().Contains(nameFilter.ToLower()) || x.Description.ToLower().Contains(nameFilter.ToLower()));
            }

            var products = await query.ToListAsync();

            return products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            }).ToList();
        }

        public async Task<List<Product>> GetAvalibleProductsAsync()
        {
            var query = _context.Product.Where(x => x.Stock > 0).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<ProductViewModel?> GetProductAsync(int id)
        {
            var product = await _context.Product.FindAsync(id);

            return product?.Id != null
                ? new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock
                }
                : null;
        }
        public async Task<Product> GetProductModelAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }
        public async Task<List<Product>> GetProductsModelAsync()
        {
            return await _context.Product.ToListAsync();
        }
        public async Task CreateProductAsync(ProductViewModel productViewModel)
        {
            var product = new Product
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Stock = productViewModel.Stock
            };

            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductViewModel productViewModel)
        {
            var existingProduct = await _context.Product.FindAsync(productViewModel.Id);

            if (existingProduct == null)
            {
                throw new InvalidOperationException("El producto no existe.");
            }

            _context.Entry(existingProduct).CurrentValues.SetValues(productViewModel);

            _context.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> DeleteValidation(int id, List<Order> orders)
        {
            foreach (var item in orders)
            {
                if (item.Products.Contains(await GetProductModelAsync(id)))
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<string?> CreateEditValidation(ProductViewModel productView)
        {
            string errorMessage = null;
            if (productView.Price <= 0)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                    errorMessage += "<br/>";

                errorMessage += "El precio debe ser mayor a 0.";
            }

            if (productView.Stock < 0)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                    errorMessage += "<br/>";

                errorMessage += "El stock debe ser igual o mayor a 0.";
            }

            return errorMessage;
        }

        public async Task ReduceStockProducts(Dictionary<int, int> ReduceStockProducts)
        {

            if (ReduceStockProducts.Any())
                foreach (var item in ReduceStockProducts)
                {
                    ProductViewModel product = await GetProductAsync(item.Key);
                    if (product != null)
                    {
                        product.Stock = product.Stock - item.Value;

                        UpdateProductAsync(product);
                    }
                }
        }

        public async Task DecreaseStockProduct(int idProduct, int stock)
        {


            ProductViewModel product = await GetProductAsync(idProduct);
            if (product != null)
            {
                product.Stock = product.Stock - stock;

                UpdateProductAsync(product);
            }
        }

        public async Task IncreaseStockProduct(int idProduct, int stock)
        {


            ProductViewModel product = await GetProductAsync(idProduct);
            if (product != null)
            {
                product.Stock = product.Stock + stock;

                UpdateProductAsync(product);
            }
        }


    }
}