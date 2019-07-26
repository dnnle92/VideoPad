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
            var video = new Video
            {
                VideoName =  "TestVideo",
                DateAdded = DateTime.Now
            };
            context.Videos.Add(video);

            var category = new Category
            {
                CatName = "Cat1"
            };
            context.Categories.Add(category);

            var videocategory = new VideoCategory
            {
                Video = video,
                Category = category
            };
            context.



            return View();
        }
    }
}