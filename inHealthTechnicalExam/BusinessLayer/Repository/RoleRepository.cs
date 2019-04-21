using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Context;
using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace inHealthTechnicalExam.BusinessLayer.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BlogDbContext _context;
        public RoleRepository(BlogDbContext context)
        {
            _context = context;
        }
        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }
        public Role GetRoleByID(int id)
        {
            return _context.Roles.Where(x => x.ID == id).FirstOrDefault();
        }
        public Role GetRoleByRoleCode(string roleCode)
        {
            return _context.Roles.Where(x => x.RoleCode == roleCode).FirstOrDefault();
        }
    }
}
