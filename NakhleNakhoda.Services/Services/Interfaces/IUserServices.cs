using NakhleNakhoda.Data.Models.DB.User;
using NakhleNakhoda.Data.Models.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakhleNakhoda.Services.Services.Interfaces
{
    public interface IUserServices
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string username);
        void UpdateUser(User user);
        bool ActiveAccount(string activeCode);
        int GetUserIdByUserName(string userName);
        void DeleteUser(int userId);
    }
}
