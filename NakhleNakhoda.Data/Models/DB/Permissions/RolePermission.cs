using NakhleNakhoda.Data.Models.DB.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB.Permissions
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role2 Role { get; set; }
        public Permission Permission { get; set; }
    }
}
