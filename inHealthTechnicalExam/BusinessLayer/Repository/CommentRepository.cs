using inHealthTechnicalExam.BusinessLayer.IRepository;
using inHealthTechnicalExam.DataAccessLayer.Context;
using inHealthTechnicalExam.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace inHealthTechnicalExam.BusinessLayer.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext _context;
        public CommentRepository(BlogDbContext context)
        {
            _context = context;
        }
        public List<Comment> GetAllCommentsByBlogID(int id)
        {
            return _context.Comments.Where(x => x.BlogID == id).ToList();
        }
        public bool CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            return _context.SaveChanges() >= 1;
        }
    }
}
