using Azure.Core;
using DTOModel;
using DTOModels.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Operation;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public  class AuthOperation  : IAuthOperation
    { 
        public readonly ProductsContext _context;
        public readonly IConfiguration _configuration;
        public AuthOperation(ProductsContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Login(LoginRequestDto dto)
        {
            try
            {
                var user=await _context.Users.Where(x=>x.Phone==dto.PhoneNumber).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new ApiResponse("200", false, new int[0], "Invalid Phone Number.");
                }

                string hash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

                Console.WriteLine(hash);
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);

                if (isPasswordValid==false)
                {
                    return new ApiResponse("200", false, new int[0], "Invalid Password.");
                }

                var userRole = await _context.Roles.Where(r=>r.Id==user.RoleId).FirstOrDefaultAsync();
                var token = GenerateToken(user.Phone);

                var verified = new LoginResponseDto { 
                    RoleId = user.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = Convert.ToString(token.Result),
                    UserEmail = user.Email,
                    UserPhone = user.Phone,
                };
                //var token ;
                return new ApiResponse("200", true, verified, "An error occurred during process.");
            }
            catch(Exception ex){
                return new ApiResponse("500", false, new int[0], "An error occurred during process.");
            }
        }


        public async Task<string> GenerateToken(string phone) {
            try
            {
                var user = await _context.Users.Where(x => x.Phone == phone).FirstOrDefaultAsync();
                var userRole = await _context.Roles.Where(r => r.Id == user.RoleId).FirstOrDefaultAsync();
                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.MobilePhone,phone),
                    new Claim(ClaimTypes.Role,userRole.RoleName),
                    new Claim(ClaimTypes.Name,user.FirstName+' '+ user.LastName)
                   //new Claim("UserId",user.Id),
                };

                var key = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
                );

                var cred = new SigningCredentials(
                        key,
                        SecurityAlgorithms.HmacSha256
                );

                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims:claim,
                        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:ExpirationMinute"])),
                        signingCredentials: cred
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

    }
}
