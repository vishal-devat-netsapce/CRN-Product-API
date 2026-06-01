using DTOModel;
using Microsoft.Extensions.Logging;
using Repository;

namespace Operation
{
    public class RoleOperation : IRoleOperation
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleOperation> _logger;

        public RoleOperation(IRoleRepository roleRepository,ILogger<RoleOperation> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<ApiResponse> AddRole(RoleDTO roleDTO)
        {
            try
            {
                return await _roleRepository.AddRole(roleDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while adding role.");

                throw;
            }
        }

        public async Task<ApiResponse> GetRoleById(int roleId)
        {
            try
            {
                return await _roleRepository.GetRoleById(roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while fetching role. RoleId: {RoleId}",roleId);

                throw;
            }
        }

        public async Task<ApiResponse> GetAllRoles()
        {
            try
            {
                return await _roleRepository.GetAllRoles();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while fetching all roles.");

                throw;
            }
        }

        public async Task<ApiResponse> UpdateRole(UpdateRoleDTO roleDTO)
        {
            try
            {
                return await _roleRepository.UpdateRole(roleDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while updating role. RoleId: {RoleId}",
                    roleDTO.Id);

                throw;
            }
        }

        public async Task<ApiResponse> DeleteRole(int roleId)
        {
            try
            {
                return await _roleRepository.DeleteRole(roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while deleting role. RoleId: {RoleId}",roleId);

                throw;
            }
        }
    }
}