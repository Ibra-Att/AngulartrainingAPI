namespace AngulartrainingAPI.DTO.User
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }=string.Empty;
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
