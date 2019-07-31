using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoSIte.Models;

namespace VideoSIte.Controllers
{
    public class VideoController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();        

        // GET: Video
        public ActionResult Index(int? id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = context.Categories.FirstOrDefault(p => p.CatId == id);
            //Category category = context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
    }
}