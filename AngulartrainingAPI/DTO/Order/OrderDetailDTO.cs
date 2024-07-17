using static AngulartrainingAPI.Helper.Enum.Enums;

namespace AngulartrainingAPI.DTO.Order
{
    public class OrderDetailDTO
    {
       
        public string Note { get; set; } = string.Empty;
        public float TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }

        public List<ProductInfoDTO> ListOfItems { get; set; }
    }
}