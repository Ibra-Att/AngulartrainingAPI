namespace AngulartrainingAPI.Models.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public float ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
