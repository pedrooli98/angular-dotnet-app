using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostsRepository : IBlogRepository
    {
        private readonly AppDbContext _context;
        public BlogPostsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(b => b.Categories).ToListAsync();
        }


        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(b => b.Id == id);

            return blogPost;
        }
    }
}