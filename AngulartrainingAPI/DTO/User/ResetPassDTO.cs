namespace AngulartrainingAPI.DTO.User
{
    public class ResetPassDTO
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
    }
}