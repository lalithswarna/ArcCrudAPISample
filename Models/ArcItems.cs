using System;
using System.Collections.Generic;

namespace ArcCrudAPI.Models
{
    public partial class ArcItems
    {
        public int ArcItemId { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
    }
}
