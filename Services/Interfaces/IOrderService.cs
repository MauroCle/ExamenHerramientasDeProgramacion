using Examenes.Models;
using Examenes.ViewModels;

namespace  Examenes.Services;

public interface IOrderService
{
    Task<List<OrderViewModel>>GetOrdersAsync(string nameFilter);
    // Task<List<OrderViewModel>> GetActiveOrdersAsync(string nameFilter);
    Task<OrderDetailViewModel> GetOrderDetailsAsync(int id);
    Task<OrderDetailsStockViewModel> GetOrderDetailsStockAsync(int id);
    Task<List<Order>> GetOrdersWithProductsAsync();
    Task<OrderEditViewModel> GetOrderEditViewModelAsync(int id);
    Task<bool> CreateOrderAsync(OrderCreateViewModel viewModel);
    Task<bool> EditOrderAsync(int id, OrderEditViewModel orderView);
    Task<Order> GetOrderAsync(int id);
    Task<bool> DeleteOrderAsync(int id);
    Task<bool> EditOrderWithoutProductsAsync(int id, OrderEditViewModel orderView); //Deprecado
}
