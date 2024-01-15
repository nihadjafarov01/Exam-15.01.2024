using Exam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam.Contexts
{
    public class ExamDbContext : IdentityDbContext
    {
        public ExamDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<LatestNewsAndBlog> LatestNewsAndBlogs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
