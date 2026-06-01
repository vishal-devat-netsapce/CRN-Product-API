using DTOModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operation;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleOperation _roleOperation;

        public RoleController(IRoleOperation roleOperation)
        {
            _roleOperation = roleOperation;
        }

        [HttpPost]
        public async Task<ApiResponse> AddRole([FromForm] RoleDTO roleDTO)
        {
            return await _roleOperation.AddRole(roleDTO);
        }

        [HttpGet("{roleId:int}")]
        public async Task<ApiResponse> GetRoleById(int roleId)
        {
            return await _roleOperation.GetRoleById(roleId);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllRoles()
        {
            return await _roleOperation.GetAllRoles();
        }

        [HttpPut]
        public async Task<ApiResponse> UpdateRole([FromForm] UpdateRoleDTO roleDTO)
        {
            return await _roleOperation.UpdateRole(roleDTO);
        }

        [HttpDelete("{roleId:int}")]
        public async Task<ApiResponse> DeleteRole(int roleId)
        {
            return await _roleOperation.DeleteRole(roleId);
        }
    }
}
