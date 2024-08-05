using AngulartrainingAPI.DataContext;
using AngulartrainingAPI.DTO.User;
using AngulartrainingAPI.Helper.Token;
using AngulartrainingAPI.Interfaces;
using AngulartrainingAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngulartrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AuthenticationController(MyDbContext context)
        {
            _context = context;
        }

       
    


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                var query = from u in _context.Users
                            where u.Email == dto.Email
                            && u.Password == dto.Password
                            select u;
                var user = await query.SingleOrDefaultAsync();
                if ( user !=null)
                {
                    var token = TokenHelper.GenerateJwtToken(user);

                    user.ISLoggedin = true;
                    _context.Update(user);
                    await _context.SaveChangesAsync();


                    var response = new LoginResponseDTO
                    {
                        Token = token,
                        Id=user.Id,
                        UserName = user.FullName,
                        Email= user.Email,
                        
                    };


                    return Ok(response);
                }
                throw new Exception("wrong email or password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong {ex.Message}");
            }

        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Logout(int id)
        {
            try
            {
                var query = from u in _context.Users
                            where u.Id == id                            
                            select u;
                var user = await query.SingleOrDefaultAsync();
                if(user != null)
                {
                    User usUp=new User()
                    {
                        ISLoggedin= false,
                    };
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                return StatusCode(200, "Successfuly logged out");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong {ex.Message}");

            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RigisterDto dto)
        {
            try
            {
                User user = new User()
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Password = dto.Password,
                    Phone = dto.Phone,
                    ProfileImage=dto.ProfileImage,
                    IsDeleted=false,
                    CreationDate=DateTime.Now,
                    ISLoggedin=false,
                    

                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return StatusCode(200, "User has been created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong {ex.Message}");

            }
        }



        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> ResetPassword(ResetPassDTO dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Phone))
                    throw new Exception("email or phone are required");
                var user = await _context.Users.Where(x => x.Email.Equals(dto.Email)
                && x.Phone.Equals(dto.Phone)).SingleOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("Incorrect provided info");
                }
                else
                {
                    if (string.IsNullOrEmpty(dto.NewPassword) || string.IsNullOrEmpty(dto.ConfirmPassword))
                        throw new Exception("Passwords does not match");
                    else
                    {
                        if (dto.NewPassword.Equals(dto.ConfirmPassword))
                        {
                            var userUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
                            userUpdate.Password = dto.ConfirmPassword;
                            _context.Update(userUpdate);
                            await _context.SaveChangesAsync();
                            return StatusCode(200, "Password successfully reseted");
                        }
                        else
                        {
                            throw new Exception("Not Mathced");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong {ex.Message}");

            }



        }


    }
}
