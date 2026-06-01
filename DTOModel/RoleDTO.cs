using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class RoleDTO
    {
        public string RoleName { get; set; } = null!;
    }

    public class UpdateRoleDTO
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = null!;
    }

}
