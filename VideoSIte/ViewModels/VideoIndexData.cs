﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoSIte.Models;

namespace VideoSIte.ViewModels
{
    public class VideoIndexData
    {
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}