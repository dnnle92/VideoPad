using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSIte.ViewModels
{
    public class CategoryViewModel
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public virtual ICollection<VideoViewModel> Videos { get; set; }
    }
}