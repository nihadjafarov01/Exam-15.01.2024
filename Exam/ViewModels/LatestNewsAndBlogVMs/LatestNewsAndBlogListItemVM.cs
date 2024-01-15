using Exam.Models;

namespace Exam.ViewModels.LatestNewsAndBlogVMs
{
    public class LatestNewsAndBlogListItemVM
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
