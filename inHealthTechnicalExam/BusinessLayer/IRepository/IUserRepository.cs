using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
namespace inHealthTechnicalExam.BusinessLayer.IRepository
{
    public interface IUserRepository
    {  
        List<User> GetAllUsers();
        bool CreateUser(User user);
        bool DeleteUserByID(int id);
        User GetUserByUsername(string username);
        User GetUserByID(int id);
        bool IsUserExist(string username);
        bool IsUserDeleted(string username);
    }
}
