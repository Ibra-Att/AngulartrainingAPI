namespace AngulartrainingAPI.DTO.Product
{
    public class ProductDetailDTO
    {
 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public string ProductImage { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}