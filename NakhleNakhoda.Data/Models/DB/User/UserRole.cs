using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB.User
{
    public  class UserRole
    {
        public UserRole()
        {

        }

        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


        #region Relations

        public virtual User User { get; set; }
        public virtual Role2 Role { get; set; }

        #endregion
    }
}
