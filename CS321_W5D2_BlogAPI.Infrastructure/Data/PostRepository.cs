using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private AppDbContext _dbContext;
        public PostRepository(AppDbContext dbContext) 
        {  
            _dbContext = dbContext;
        }

        public Post Get(int id)
        {
            return _dbContext.Posts
                .Include(p => p.Blog)
                .Include(p => p.Blog.User)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return _dbContext.Posts
                .Include(p => p.Blog)
                .Include(p => p.Blog.User)
                .Where(p => p.BlogId == blogId)
                .ToList();
        }

        public Post Add(Post post)
        {
            _dbContext.Add(post);
            _dbContext.SaveChanges();
            return post;
        }

        public Post Update(Post updatedpost)
        {
             var currentPost = _dbContext.Posts.Find(updatedpost.Id);

            if (currentPost == null) return null;

            _dbContext.Entry(currentPost)
                .CurrentValues
                .SetValues(updatedpost);

            _dbContext.Posts.Update(currentPost);
            _dbContext.SaveChanges();
            return currentPost;
        }

        public IEnumerable<Post> GetAll()
        {
            return _dbContext.Posts
                .ToList();
        }

        public void Remove(int id)
        {
            // TODO: remove Post
            var delPost = _dbContext.Posts.Find(id);

            if (delPost != null)
            {
                _dbContext.Posts.Remove(delPost);
                _dbContext.SaveChanges();
            }
        }

    }
}
