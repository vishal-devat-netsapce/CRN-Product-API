using DTOModel;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class AuthRepository : IAuthRepository
    {
        public readonly ProductsContext _productsContext;
        public AuthRepository(ProductsContext productsContext) 
        {
             _productsContext = productsContext;
        }

        public async Task<ApiResponse> AddUser(AddUserDTO userDTO)
        {
           bool isExist = await _productsContext.Users.AnyAsync(x => x.Phone == userDTO.Phone &&  x.RoleId==userDTO.RoleId );

            if (isExist==true)
            {
                return new ApiResponse("400",false,null,"User already registered for this role");
            }
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            User userData = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                RoleId = userDTO.RoleId,
                Phone = userDTO.Phone,
                PasswordHash= hashPassword,
                CreatedOn = DateTime.Now,
            };

            await _productsContext.Users.AddAsync(userData);
            await  _productsContext.SaveChangesAsync();

            var userRole = await _productsContext.Roles.Where(x => x.Id == userData.RoleId).Select(x => x.RoleName).FirstOrDefaultAsync();

            var result = new UserResponseDTO
            {
                Id = userData.Id,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                RoleId = userData.RoleId,
                RoleName = userRole,
                Phone = userData.Phone,
                CreatedOn = userData.CreatedOn
            };
            return new ApiResponse("200", true, result, "User registered successfully");
        }

    }
}
