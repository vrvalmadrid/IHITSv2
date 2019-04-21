using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Context;
using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace inHealthTechnicalExam.BusinessLayer.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _context;
        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }
        public List<Blog> GetAllBlogs()
        {
            return _context.Blogs.Where(x => x.IsDeleted == false).ToList();
        }
        public bool CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            return _context.SaveChanges() >= 1;
        }
        public Blog GetBlogByID(int id)
        {
            return _context.Blogs.Where(x => x.ID == id).FirstOrDefault();
        }
        public bool DeleteBlogByID(int id)
        {
            var blog = GetBlogByID(id);
            blog.IsDeleted = true;
            _context.Blogs.Update(blog);
            return _context.SaveChanges() >= 1;
        }

        public bool IsBlogExist(int id)
        {
            return (GetBlogByID(id) == null) ?false:true;
        }
    }
}
