using static AngulartrainingAPI.Helper.Enum.Enums;

namespace AngulartrainingAPI.DTO.Order
{
    public class OrderCardDTO
    {
        public int Id { get; set; }
        public float TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
    }
}