using AngulartrainingAPI.DTO.Order;
using AngulartrainingAPI.DTO.OrderProduct;
using AngulartrainingAPI.DTO.Product;
using AngulartrainingAPI.DTO.User;

namespace AngulartrainingAPI.Interfaces
{
    public interface IMain
    {
        ////user
        //Task UpdateUserProfile(UpdateUserDTO dto);


        //product
        Task<List<ProductCardDTO>> GetAllProduct();
        Task<ProductDetailDTO> GetProductById(int id);

       // Task AddProduct(CreateProductDTO dto);
 
        Task UpdateProduct(UpdateProduct dto);

        Task DeleteProductById(int id);

        //Order
        Task<List<OrderCardDTO>> GetAllOrders();
        Task<OrderDetailDTO> GetOrderById(int id);
        Task CreateOrder(CreateOrderDTO dto);
        Task UpdateOrder(UpdateOrderDTO dto);

        //OrderProduct
        Task<List<OrderProductDTO>> GetAllOrderProducts();
        Task <OrderProductDetailsDTO>GetOrderProductByOrderID(int orderId);





    }
}
