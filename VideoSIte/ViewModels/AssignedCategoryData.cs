using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoSIte.Models;

namespace VideoSIte.ViewModels
{
    public class AssignedCategoryData
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public bool Assigned { get; set; }
    }
}