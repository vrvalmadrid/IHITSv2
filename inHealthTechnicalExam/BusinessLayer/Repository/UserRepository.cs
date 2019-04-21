using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Context;
using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace inHealthTechnicalExam.BusinessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _context;
        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.Where(x => x.IsDeleted == false).ToList();
        }
        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() >= 1;
        }
        public User GetUserByUsername(string username)
        {
            return _context.Users.Where(x => x.Username == username).FirstOrDefault();
        }
        public bool IsUserExist(string username)
        {
            return GetUserByUsername(username) != null ?true:false;
        }
        public User GetUserByID(int id)
        {
            return _context.Users.Where(x => x.ID == id).FirstOrDefault();
        }
        public bool DeleteUserByID(int id)
        {
            var user = GetUserByID(id);
            user.IsDeleted = true;
            _context.Users.Update(user);
            return _context.SaveChanges() >= 1;
        }
        public bool IsUserDeleted(string username)
        {
            return GetUserByUsername(username).IsDeleted == true ? true : false;
        }
    }
}
