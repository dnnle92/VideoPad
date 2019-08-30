using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSIte.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}