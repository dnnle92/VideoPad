using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSIte.Models;

namespace VideoSIte.Controllers
{
    public class VideoController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        var video = context
        // GET: Video
        public ActionResult Index()
        {
            Video video = new Video()
            {
                VideoName = "TestVideo",
                DateAdded = DateTime.Now
            };
            context.Videos.Add(video);
            context.SaveChanges();
            return View();
        }
    }
}