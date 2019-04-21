using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Models;
using inHealthTechnicalExam.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace inHealthTechnicalExam.Controllers
{   
    public class BlogsController : Controller
    {
        private IUserRepository UserRepository { get; set; }
        private IRoleRepository RoleRepository { get; set; }
        private IUserRoleRepository UserRoleRepository { get; set; }
        private IBlogRepository BlogRepository { get; set; }
        public BlogsController(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository,IBlogRepository blogRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserRoleRepository = userRoleRepository;
            BlogRepository = blogRepository;
        }
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var user = UserRepository.GetUserByUsername(User.Identity.Name);
                var userRole = UserRoleRepository.GetAllUserRolesByUserID(user.ID).FirstOrDefault();
                var role = RoleRepository.GetRoleByID(userRole.RoleID);
                ViewBag.IsAdmin = new Utility().IsUserHasAdminRole(role.RoleCode);
            }
            else
            {
                ViewBag.IsAdmin = false;
            }
            return View(BlogRepository.GetAllBlogs().ToList());
        }
        [Authorize(Roles = "ADMIN,STANDARD")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "ADMIN,STANDARD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.CreatedDate = DateTime.Now;
                blog.CreatedBy = UserRepository.GetUserByUsername(User.Identity.Name).ID;
                blog.IsDeleted = false;
                BlogRepository.CreateBlog(blog);
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            return View(BlogRepository.GetBlogByID(id));
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            BlogRepository.DeleteBlogByID(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
