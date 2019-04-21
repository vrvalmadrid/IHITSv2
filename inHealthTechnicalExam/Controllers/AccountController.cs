using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Models;
using inHealthTechnicalExam.DataAccessLayer.Models.ViewModels;
using inHealthTechnicalExam.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace inHealthTechnicalExam.Controllers
{
    public class AccountController : Controller
    {
        private  IUserRepository UserRepository { get; set; }
        private  IRoleRepository RoleRepository { get; set; }
        private  IUserRoleRepository UserRoleRepository { get; set; }
        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserRoleRepository = userRoleRepository;
        }
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!UserRepository.IsUserExist(registerViewModel.Username))
                {
                    User newUser = new User { Username = registerViewModel.Username, Password = new Utility().Encrypt(registerViewModel.Password), Name = registerViewModel.Name, IsDeleted = false };
                    UserRepository.CreateUser(newUser);
                    if (IsFirstUser())
                    {
                        UserRole newAdminUserRole = new UserRole { UserID = newUser.ID, RoleID = RoleRepository.GetRoleByRoleCode(Constant.ADMIN).ID };
                        UserRoleRepository.CreateUserRole(newAdminUserRole);

                    }
                    UserRole newStandardUserRole = new UserRole { UserID = newUser.ID, RoleID = RoleRepository.GetRoleByRoleCode(Constant.STANDARD).ID };
                    UserRoleRepository.CreateUserRole(newStandardUserRole);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username already exist");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = null;
                if (UserRepository.IsUserExist(loginViewModel.Username))
                {
                    if(!UserRepository.IsUserDeleted(loginViewModel.Username))
                    {
                        var user = UserRepository.GetUserByUsername(loginViewModel.Username);
                        user.Password = new Utility().Decrypt(user.Password);
                        if (user.Password == loginViewModel.Password)
                        {
                            var userrole = UserRoleRepository.GetAllUserRolesByUserID(user.ID).FirstOrDefault();
                            var role = RoleRepository.GetRoleByID(userrole.RoleID);
                            identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, loginViewModel.Username), new Claim(ClaimTypes.Role, role.RoleCode) }, CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthorizeAttribute authorizeattribute = new AuthorizeAttribute() { Roles = role.RoleCode };
                            var principal = new ClaimsPrincipal(identity);
                            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["UserLoginFailed"] = "Login Failed. Cann't access the account. Please contact your administrator.";
                        return View();
                    }                    
                }
                else
                {
                    TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                    return View();
                }
            }
            else
                return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public bool IsFirstUser()
        {
            return (UserRepository.GetAllUsers().Count() == 1) ? true : false;
        }
    }
}