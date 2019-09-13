using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSIte.ViewModels
{
    public class AssignedVideoData
    {
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        public bool Assigned { get; set; }
    }
}