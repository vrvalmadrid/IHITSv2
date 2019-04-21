using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
namespace inHealthTechnicalExam.BusinessLayer.IRepository
{
    public interface IUserRoleRepository
    {
        List<UserRole> GetAllUserRoles();
        bool CreateUserRole(UserRole userRole);
        bool DeleteUserRoleByID(int userId);
        List<UserRole> GetAllUserRolesByUserID(int userId);
    }
}
