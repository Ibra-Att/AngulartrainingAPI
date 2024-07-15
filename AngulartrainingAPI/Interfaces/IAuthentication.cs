using AngulartrainingAPI.DTO.User;

namespace AngulartrainingAPI.Interfaces
{
    public interface IAuthentication
    {
        Task Register(RigisterDto dto);
        Task<string> Login(LoginDTO dto );
        Task ResetPassword(ResetPassDTO dto);
        Task Logout(int id);
    }
}
