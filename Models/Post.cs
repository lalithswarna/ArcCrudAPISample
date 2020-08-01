using System;
using System.Collections.Generic;

namespace ArcCrudAPI.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ItemId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Item Item { get; set; }
    }
}
