using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
namespace inHealthTechnicalExam.BusinessLayer.IRepository
{
    public interface IBlogRepository
    {
        List<Blog> GetAllBlogs();
        bool CreateBlog(Blog blog);
        bool DeleteBlogByID(int id);
        Blog GetBlogByID(int id);
        bool IsBlogExist(int id);
    }
}
