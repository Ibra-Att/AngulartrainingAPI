namespace AngulartrainingAPI.DTO.User
{
    public class RigisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
        public string? ProfileImage { get; set; }
    }
}