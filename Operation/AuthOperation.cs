using DTOModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Operation
{
    public class AuthOperation : IAuthOperation
    {
        private readonly ProductsContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthOperation> _logger;

        public AuthOperation(
            ProductsContext context,
            IAuthRepository authRepository,
            IConfiguration configuration,
            ILogger<AuthOperation> logger)
        {
            _context = context;
            _configuration = configuration;
            _authRepository = authRepository;
            _logger = logger;
        }

        public async Task<ApiResponse> AddUser(AddUserDTO dto)
        {
            try
            {
                return await _authRepository.AddUser(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user. Phone: {Phone}", dto.Phone);
                throw;
            }
        }

        public async Task<ApiResponse> Login(LoginRequestDto dto)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Phone == dto.PhoneNumber);

                if (user == null)
                {
                    return new ApiResponse("200", false, new int[0], "Invalid Phone Number.");
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

                if (!isPasswordValid)
                {
                    return new ApiResponse("200", false, new int[0], "Invalid Password.");
                }

                var userRole = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Id == user.RoleId);

                if (userRole == null)
                {
                    return new ApiResponse("200", false, new int[0], "User role not found.");
                }

                var token = await GenerateToken(user.Phone);

                var verified = new LoginResponseDto
                {
                    RoleId = user.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token,
                    UserEmail = user.Email,
                    UserPhone = user.Phone
                };

                return new ApiResponse("200", true, verified, "Login successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login. Phone: {Phone}", dto.PhoneNumber);
                throw;
            }
        }

        public async Task<string> GenerateToken(string phone)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Phone == phone);

                if (user == null)
                {
                    throw new Exception("User not found while generating token.");
                }

                var userRole = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Id == user.RoleId);

                if (userRole == null)
                {
                    throw new Exception("User role not found while generating token.");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.MobilePhone, phone),
                    new Claim(ClaimTypes.Role, userRole.RoleName),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim("UserId", user.Id.ToString())
                };

                var jwtSecret = _configuration["JWT:Secret"];

                if (string.IsNullOrWhiteSpace(jwtSecret))
                {
                    throw new Exception("JWT Secret key is missing in appsettings.json.");
                }

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSecret)
                );

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(
                        Convert.ToDouble(_configuration["JWT:ExpirationMinute"])
                    ),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating token. Phone: {Phone}", phone);
                throw;
            }
        }
    }
}