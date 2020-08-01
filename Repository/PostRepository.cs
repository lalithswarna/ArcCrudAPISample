using ArcCrudAPI.Models;
using ArcCrudAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcCrudAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        ArcDBContext db;
        public PostRepository(ArcDBContext _db)
        {
            db = _db;
        }

        public async Task<List<Item>> GetCategories()
        {
            if (db != null)
            {
                return await db.Item.ToListAsync();
            }

            return null;
        }

        public async Task<List<PostViewModel>> GetPosts()
        {
            if (db != null)
            {
                return await (from p in db.Post
                              from c in db.Item
                              where p.ItemId == c.ItemId
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  Title = p.Title,
                                  Description = p.Description,
                                  CategoryId = p.ItemId,
                                  CategoryName = c.Title,
                                  CreatedDate = p.CreatedDate
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<PostViewModel> GetPost(int? postId)
        {
            if (db != null)
            {
                return await (from p in db.Post
                              from c in db.Item
                              where p.PostId == postId
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  Title = p.Title,
                                  Description = p.Description,
                                  CategoryId = p.ItemId,
                                  CategoryName = c.Title,
                                  CreatedDate = p.CreatedDate
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddPost(Post post)
        {
            if (db != null)
            {
                await db.Post.AddAsync(post);
                await db.SaveChangesAsync();

                return post.PostId;
            }

            return 0;
        }

        public async Task<int> DeletePost(int? postId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var post = await db.Post.FirstOrDefaultAsync(x => x.PostId == postId);

                if (post != null)
                {
                    //Delete that post
                    db.Post.Remove(post);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdatePost(Post post)
        {
            if (db != null)
            {
                //Delete that post
                db.Post.Update(post);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

    }
}
