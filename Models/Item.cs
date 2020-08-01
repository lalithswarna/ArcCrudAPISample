using System;
using System.Collections.Generic;

namespace ArcCrudAPI.Models
{
    public partial class Item
    {
        public Item()
        {
            Post = new HashSet<Post>();
        }

        public int ItemId { get; set; }
        public string Title { get; set; }
        public int? Cost { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
