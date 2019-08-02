using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSIte.ViewModels
{
    public class VideoViewModel
    {
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        public virtual ICollection<AssignedCategoryData> Categories { get; set; }

    }

}