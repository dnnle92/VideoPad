using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoSIte.Models;

namespace VideoSIte.Controllers
{
    public class VideosController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Videos
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CategoryPartial()
        {
            var categoryList = db.Categories.OrderBy(x => x.CatName).ToList();
            return PartialView(categoryList);
        }

        public PartialViewResult VideoListPartial(int? category)
        {
            if (category != null)
            {
                var videosList = db.Videos.OrderByDescending(a => a.VideoId).Where(a => a.Categories.Any(c => c.CatId == category)).ToList();
                return PartialView(videosList);
            }
            else
            {
                var videosList = db.Videos.OrderByDescending(a => a.VideoId).ToList();
                return PartialView(videosList);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }
    }
}