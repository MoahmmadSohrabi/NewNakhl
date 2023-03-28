using NakhleNakhoda.Data.Models.DB.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB.User
{
    public class Role2
    {
        public Role2()
        {

        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }

        public bool IsDelete { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }


        #endregion
    }
}
