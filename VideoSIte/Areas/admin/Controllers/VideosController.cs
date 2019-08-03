using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoSIte.Models;
using VideoSIte.ViewModels;

namespace VideoSIte.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Videos
        public ActionResult Index(int? id)
        {
            var viewModel = new VideoIndexData();
            viewModel.Videos = db.Videos
                .Include(i => i.Categories);
            if (id != null)
            {
                ViewBag.VideoId = id.Value;
                viewModel.Categories = viewModel.Videos.Where(
                    i => i.VideoId == id.Value).Single().Categories;
            }
            return View(viewModel);

            //var videos = db.Videos.Include(p => p.Categories);
            //return View(videos.ToList());
        }

        // GET: Videos/Details/5
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

        // GET: Videos/Create
        public ActionResult Create()
        {
            var video = new Video();
            video.Categories = new List<Category>();
            PopulateAssignedCategoryData(video);
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideoId,VideoName,DateAdded")] Video video, string[] selectedCategories)
        {
            if (selectedCategories != null)
            {
                video.Categories = new List<Category>();
                foreach (var category in selectedCategories)
                {
                    var categoryToAdd = db.Categories.Find(int.Parse(category));
                    video.Categories.Add(categoryToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCategoryData(video);
            return View(video);
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos
                .Include(i => i.Categories)
                .Where(i => i.VideoId == id)
                .Single();
            PopulateAssignedCategoryData(video);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        private void PopulateAssignedCategoryData(Video video)
        {
            var allCategories = db.Categories;
            var videoCategories = new HashSet<int>(video.Categories.Select(c => c.CatId));
            var viewModel = new List<AssignedCategoryData>();
            foreach (var category in allCategories)
            {
                viewModel.Add(new AssignedCategoryData
                {
                    CatId = category.CatId,
                    CatName = category.CatName,
                    Assigned = videoCategories.Contains(category.CatId)
                });
            }
            ViewBag.Categories = viewModel;
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var videoToUpdate = db.Videos
                .Include(i => i.Categories)
                .Where(i => i.VideoId == id)
                .Single();
            if (TryUpdateModel(videoToUpdate, "",
                new string[] { "VideoId", "VideoName", "DateAdded" }))
            {
                try
                {
                    UpdateVideoCategories(selectedCategories, videoToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException  )
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }
            PopulateAssignedCategoryData(videoToUpdate);
            return View(videoToUpdate);
        }

        private void UpdateVideoCategories(string[] selectedCategories, Video videoToUpdate)
        {
            if (selectedCategories == null)
            {
                videoToUpdate.Categories = new List<Category>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var videoCategories = new HashSet<int>
                (videoToUpdate.Categories.Select(c => c.CatId));
            foreach (var category in db.Categories)
            {
                if (selectedCategoriesHS.Contains(category.CatId.ToString()))
                {
                    if (!videoCategories.Contains(category.CatId))
                    {
                        videoToUpdate.Categories.Add(category);
                    }
                }
                else
                {
                    if (videoCategories.Contains(category.CatId))
                    {
                        videoToUpdate.Categories.Remove(category);
                    }
                }
            }
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
