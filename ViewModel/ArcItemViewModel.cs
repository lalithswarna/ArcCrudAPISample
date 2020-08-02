using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcCrudAPI.ViewModel
{
    public class ArcItemViewModel
    {
        public int ArcItemID { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
    }
}
