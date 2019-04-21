using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Context;
using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace inHealthTechnicalExam.BusinessLayer.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly BlogDbContext _context;
        public UserRoleRepository(BlogDbContext context)
        {
            _context = context;
        }
        public List<UserRole> GetAllUserRoles()
        {
            return _context.UserRoles.ToList();
        }
        public bool CreateUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            return _context.SaveChanges() >= 1;
        }
        public List<UserRole> GetAllUserRolesByUserID(int userId)
        {
            return _context.UserRoles.OrderBy(x => x.RoleID).Where(x => x.UserID == userId).ToList();
        }
        public bool DeleteUserRoleByID(int userId)
        {
            _context.UserRoles.RemoveRange(GetAllUserRolesByUserID(userId));
            return _context.SaveChanges() >= 1;
        }
    }
}
