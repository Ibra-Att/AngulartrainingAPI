using static AngulartrainingAPI.Helper.Enum.Enums;

namespace AngulartrainingAPI.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Note { get; set; }=string.Empty;
        public float TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.NONE;
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }=DateTime.Now;
    }
}
