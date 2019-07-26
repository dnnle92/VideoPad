using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSIte.Models;


namespace VideoSIte.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Video(int id)
        {


            Video video = new Video()
            {
                VideoName = "https://www.youtube.com/watch?v=9TBTPf_TiPk",
                DateAdded = DateTime.Now

            };
            VideoCategory videoCategory = new VideoCategory()
            {
                CatName = "Sports"
            };

            context.Videos.Add(video);
            context.VideoCategories.Add(videoCategory);
            context.SaveChanges();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}