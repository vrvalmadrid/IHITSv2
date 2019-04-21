using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
namespace inHealthTechnicalExam.BusinessLayer.IRepository
{
    public interface ICommentRepository
    {
        List<Comment> GetAllCommentsByBlogID(int id);
        bool CreateComment(Comment comment);
    }
}
