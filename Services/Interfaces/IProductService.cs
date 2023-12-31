using Examenes.Models;
using Examenes.ViewModels;

namespace  Examenes.Services;
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetProductsAsync(string nameFilter);
        Task<List<Product>> GetAvalibleProductsAsync();
        Task<ProductViewModel> GetProductAsync(int id);
        Task<Product> GetProductModelAsync(int id);
        Task<List<Product>> GetProductsModelAsync();
        Task CreateProductAsync(ProductViewModel productViewModel);
        Task UpdateProductAsync(ProductViewModel productViewModel);
        Task DeleteProductAsync(int id);
        Task<bool> DeleteValidation(int id, List<Order> orders);
        Task<string?> CreateEditValidation(ProductViewModel productView);
        Task ReduceStockProducts(Dictionary<int,int> ReduceStockProducts);
        Task IncreaseStockProduct(int idProduct, int stock);
        Task DecreaseStockProduct(int idProduct, int stock);
    }
