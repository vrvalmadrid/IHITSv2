using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
namespace inHealthTechnicalExam.BusinessLayer.IRepository
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
        Role GetRoleByID(int id);
        Role GetRoleByRoleCode(string roleCode);
    }
}
