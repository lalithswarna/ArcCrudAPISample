using ArcCrudAPI.Models;
using ArcCrudAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcCrudAPI.Repository
{
    public interface IItemRepository
    {
        Task<List<ArcItems>> GetArcItems();

        Task<ArcItemViewModel> GetItem(int? postId);

        Task<int> AddItem(ArcItems post);

        Task<int> DeleteItem(int? postId);

        Task UpdateItem(ArcItems post);
    }
}
