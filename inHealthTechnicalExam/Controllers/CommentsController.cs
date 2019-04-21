using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace inHealthTechnicalExam.Controllers
{
    public class CommentsController : Controller
    {
        private ICommentRepository CommentRepository { get; set; }
        private IBlogRepository BlogRepository { get; set; }
        public CommentsController(ICommentRepository commentRepository, IBlogRepository blogRepository)
        {
            CommentRepository = commentRepository;
            BlogRepository = blogRepository;
        }
        public IActionResult Create(int id)
        {
            if(!BlogRepository.IsBlogExist(id))
                return RedirectToAction("Index", "Blog");
            SetBlogToComment(id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedDate = DateTime.Now;
                comment.BlogID = comment.ID;
                comment.ID = 0;
                CommentRepository.CreateComment(comment);
                return RedirectToAction("Create", "Comments", comment.BlogID);
            }
            SetBlogToComment(comment.ID);
            return View(comment);
        }

        public void SetBlogToComment(int id)
        {
            var blog = BlogRepository.GetAllBlogs().Where(x => x.ID == id);
            ViewData["listComments"] = CommentRepository.GetAllCommentsByBlogID(id);
            ViewData["BlogID"] = new SelectList(blog, "ID", "Title");
        }
    }
}
