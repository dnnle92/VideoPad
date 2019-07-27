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

        // GET: Video
        public ActionResult Index()
        {

            //var video = new Video
            //{
            //    VideoName = "TennisVideo",
            //    DateAdded = DateTime.Now
            //};
            //video.Categories.Add(new Category
            //{
            //    CatName = "Sports"
            //});
            //video.Categories.Add(new Category
            //{
            //    CatName = "Tennis"
            //});

            //context.Videos.Add(video);
            //context.SaveChanges();

            return View();
        }


    }
}