namespace AngulartrainingAPI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ProfileImage {  get; set; } = string.Empty;
        public bool ISLoggedin { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
