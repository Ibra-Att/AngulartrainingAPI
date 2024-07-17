using AngulartrainingAPI.DataContext;
using AngulartrainingAPI.DTO.User;
using AngulartrainingAPI.Models.Entities;

namespace AngulartrainingAPI.Interfaces
{
    public interface IAuthentication
    {
        
        Task<string> GenerateUserAccessToken(User input);
        Task<User> TryAuthenticate(LoginDTO dto);
        Task<int> GetUserIdAfterLogin(string email, string password);

        Task Register(RigisterDto dto);
        Task<string> Login(LoginDTO dto );
        Task ResetPassword(ResetPassDTO dto);
        Task Logout(int id);
    }
}
