using ArcCrudAPI.Models;
using ArcCrudAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcCrudAPI.Repository
{
    public class ItemRepository : IItemRepository
    {
        ArcDBContext db;
        public ItemRepository(ArcDBContext _db)
        {
            db = _db;
        }

        public async Task<List<ArcItems>> GetArcItems()
        {
            if (db != null)
            {
                return await db.ArcItems.ToListAsync();
            }

            return null;
        }

        public async Task<ArcItemViewModel> GetItem(int? postId)
        {
            if (db != null)
            {
                return await (
                              from c in db.ArcItems
                              where c.ArcItemId == postId
                              select new ArcItemViewModel
                              {
                                  ArcItemID = c.ArcItemId,
                                  Title = c.Title,
                                  Quantity = c.Quantity,
                                  Cost = c.Cost
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddItem(ArcItems post)
        {
            if (db != null)
            {
                await db.ArcItems.AddAsync(post);
                await db.SaveChangesAsync();

                return post.ArcItemId;
            }

            return 0;
        }

        public async Task<int> DeleteItem(int? postId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var post = await db.ArcItems.FirstOrDefaultAsync(x => x.ArcItemId == postId);

                if (post != null)
                {
                    //Delete that post
                    db.ArcItems.Remove(post);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateItem(ArcItems post)
        {
            if (db != null)
            {
                //Delete that post
                db.ArcItems.Update(post);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

    }
}
