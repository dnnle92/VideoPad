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
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: admin/Products
        public ActionResult Index(int? id)
        {
            var viewModel = new VideoIndexData();
            viewModel.Products = db.Products
                .Include(i => i.Videos);
            if (id != null)
            {
                ViewBag.VideoId = id.Value;
                viewModel.Videos = viewModel.Products.Where(
                    i => i.ProductId == id.Value).Single().Videos;
            }
            return View(viewModel);
        }

        // GET: admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/Products/Create
        public ActionResult Create()
        {
            var product = new Product();
            product.Videos = new List<Video>();
            PopulateAssignedVideoData(product);
            return View();
        }

        // POST: admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductImage,DateAdded")] Product product, string [] selectedVideos)
        {
            if (selectedVideos != null)
            {
                product.Videos = new List<Video>();
                foreach (var Video in selectedVideos)
                {
                    var videoToAdd = db.Videos.Find(int.Parse(Video));
                    product.Videos.Add(videoToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedVideoData(product);
            return View(product);
        }

        // GET: admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products
                .Include(i => i.Videos)
                .Where(i => i.ProductId == id)
                .Single();
            PopulateAssignedVideoData(product);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        private void PopulateAssignedVideoData(Product product)
        {
            var allVideos = db.Videos;
            var productVideos = new HashSet<int>(product.Videos.Select(v => v.VideoId));
            var viewModel = new List<AssignedVideoData>();
            foreach (var video in allVideos)
            {
                viewModel.Add(new AssignedVideoData
                {
                    VideoId = video.VideoId,
                    VideoName = video.VideoName,
                    Assigned = productVideos.Contains(video.VideoId)
                });
            }
            ViewBag.Videos = viewModel;
        }

        // POST: admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedVideos)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productToUpdate = db.Products
                .Include(i => i.Videos)
                .Where(i => i.ProductId == id)
                .Single();
            if (TryUpdateModel(productToUpdate, "",
                new string[] { "ProductId", "ProductName", "ProductImage", "DateAdded" }))
            {
                try
                {
                    UpdateProductVideos(selectedVideos, productToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException )
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");             
                }
            }
            PopulateAssignedVideoData(productToUpdate);
            return View(productToUpdate);
        }

        private void UpdateProductVideos(string[] selectedVideos, Product productToUpdate)
        {
            if (selectedVideos == null)
            {
                productToUpdate.Videos = new List<Video>();
                return;
            }

            var selectedVideosHS = new HashSet<string>(selectedVideos);
            var productVideos = new HashSet<int>
                (productToUpdate.Videos.Select(v => v.VideoId));
            foreach (var video in db.Videos)
            {
                if (selectedVideosHS.Contains(video.VideoId.ToString()))
                {
                    if (!productVideos.Contains(video.VideoId))
                    {
                        productToUpdate.Videos.Add(video);
                    }
                }
                else
                {
                    if (productVideos.Contains(video.VideoId))
                    {
                        productToUpdate.Videos.Remove(video);
                    }
                }
            }
        }

        // GET: admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
