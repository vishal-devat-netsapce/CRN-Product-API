using DTOModel;
using Microsoft.AspNetCore.Http;
using Operation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthOperation _auth;
        public  AuthController(IConfiguration configuration, IAuthOperation auth) 
        {
            _configuration = configuration;
            _auth= auth;
        }

        [HttpPost("Registration")]
        [AllowAnonymous]
        public async Task<ApiResponse> UserRegistration([FromForm] AddUserDTO userDTO)
        {
            return await _auth.AddUser(userDTO);
        }


        [HttpPost("Login")]
      
        [AllowAnonymous]
        public async Task<ApiResponse>  Login([FromForm] LoginRequestDto loginDTO) {
            try
            {
                return await _auth.Login(loginDTO);
               
            }
            catch (Exception ex)
            {
                return new ApiResponse("500", false, new int[0],"An error occurred during process.");
            }
        }


    }
}
