using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using VideoSIte.Models;
using VideoSIte.ViewModels;

namespace VideoSIte.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? id)
        {
            var viewModel = new VideoIndexData();
            viewModel.Categories = db.Categories
                .Include(i => i.Videos);
            if (id != null)
            {
                ViewBag.CatId = id.Value;
                viewModel.Videos = viewModel.Categories.Where(
                    i => i.CatId == id.Value).Single().Videos;
            }
            return View(viewModel);
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