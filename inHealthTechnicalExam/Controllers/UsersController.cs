using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Models;
using inHealthTechnicalExam.DataAccessLayer.Models.ViewModels;
using inHealthTechnicalExam.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace inHealthTechnicalExam.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepository UserRepository { get; set; }
        private IRoleRepository RoleRepository { get; set; }
        private IUserRoleRepository UserRoleRepository { get; set; }
        public UsersController(IUserRepository userRepository,IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserRoleRepository = userRoleRepository;
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {            
            return View(GetListUsers());
        }
        [Authorize(Roles = "ADMIN")]
        public  IActionResult Delete(int id)
        {
            if (UserRepository.GetUserByID(id) == null)
                return RedirectToAction("Index", "User");
            return View(UserRepository.GetUserByID(id));
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = UserRepository.GetUserByID(id);
            var userRole = UserRoleRepository.GetAllUserRolesByUserID(user.ID).FirstOrDefault();
            var role = RoleRepository.GetRoleByID(userRole.RoleID);
            if (role.RoleCode != Constant.ADMIN)
            {
                UserRepository.DeleteUserByID(user.ID);
            }
            else
            {
                TempData["UserDeleteFailed"] = "Delete Failed. Cann't delete user with same ADMIN role access.";
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult GiveAdminAccess(int id)
        {
            if (UserRepository.GetUserByID(id) == null)
                return RedirectToAction("Index", "User");
            var userRole = UserRoleRepository.GetAllUserRolesByUserID(id).FirstOrDefault();
            var role = RoleRepository.GetRoleByID(userRole.RoleID); 
            if (role.RoleCode == Constant.STANDARD)
            {
                UserRole newUserRole = new UserRole { UserID = id, RoleID = RoleRepository.GetRoleByRoleCode(Constant.ADMIN).ID };
                UserRoleRepository.CreateUserRole(newUserRole);
            }
            return RedirectToAction(nameof(Index));
        }

        public List<UserAccessRoleViewModel> GetListUsers()
        {
            var userDetails = from a in UserRepository.GetAllUsers()
                              join b in UserRoleRepository.GetAllUserRoles() on a.ID equals b.UserID
                              join c in RoleRepository.GetAllRoles() on b.RoleID equals c.ID
                              where a.IsDeleted == false
                              select new UserAccessRoleViewModel
                              {
                                  ID = a.ID,
                                  Username = a.Username,
                                  Name = a.Name,
                                  Roles = c.RoleCode
                              };
            List<UserAccessRoleViewModel> listUserAccessRoleViewModel = new List<UserAccessRoleViewModel>();
            UserAccessRoleViewModel item = new UserAccessRoleViewModel();
            foreach (var users in UserRepository.GetAllUsers())
            {
                item = new UserAccessRoleViewModel { ID = users.ID, Username = users.Username, Name = users.Name };
                item.Roles = "";
                foreach (var userRole in UserRoleRepository.GetAllUserRolesByUserID(users.ID))
                {
                    foreach (var role in RoleRepository.GetAllRoles().Where(x => x.ID == userRole.RoleID))
                    {
                        item.Roles += role.RoleCode + ",";
                    }
                }
                item.Roles = item.Roles.Substring(0, item.Roles.Length - 1);
                listUserAccessRoleViewModel.Add(item);
            }
            return listUserAccessRoleViewModel;
        }
    }
}
