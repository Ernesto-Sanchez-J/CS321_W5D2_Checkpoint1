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
        public PostRepository(AppDbContext dbContext) 
        {  
            _dbContext = dbContext;
        }

        public Post Get(int id)
        {
            // TODO: Implement Get(id). Include related Blog and Blog.User
            return _dbContext.Posts
                .Include(p => p.Blog)
                .Include(p => p.Blog.User)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // TODO: Implement GetBlogPosts, return all posts for given blog id
            // TODO: Include related Blog and AppUser
            return _dbContext.Posts
                .Include(p => p.Blog)
                .Include(p => p.Blog.User)
                .Where(p => p.BlogId == blogId)
                .ToList();
        }

        public Post Add(Post Post)
        {
            // TODO: add Post
            _dbContext.Add(post);
            _dbContexr.SaveChanges();
            return post;
        }

        public Post Update(Post Post)
        {
            // TODO: update Post
             var currentPost = _dbContext.Posts.Find(updatedPost.Id);

            if (currentPost == null) return null;

            _dbContext.Entry(currentPost)
                .CurrentValues
                .SetValues(updatedPost);

            _dbContext.Posts.Update(currentPost);
            _dbContext.SaveChanges();
            return currentPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // TODO: get all posts
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
