using static AngulartrainingAPI.Helper.Enum.Enums;

namespace AngulartrainingAPI.DTO.Order
{
    public class UpdateOrderDTO
    {
        public int Id { get; set; }
        public string Note { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
       
    }
}