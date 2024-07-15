namespace AngulartrainingAPI.DTO.Product
{
    public class ProductCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public string ProductImage { get; set; } = string.Empty;
    }
}