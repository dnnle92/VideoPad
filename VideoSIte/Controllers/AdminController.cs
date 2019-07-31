using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoSIte.Models;

namespace VideoSIte.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      id = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserViewModel()

                                  {
                                      Id = p.id,  
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            return View(usersWithRoles);
        }
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            string userName = form["txtEmail"];
            string email = form["txtEmail"];
            string pwd = form["txtPassword"];

            //Create Default Users
            var user = new ApplicationUser();
            user.UserName = userName;
            user.Email = email;

            userManager.Create(user, pwd);

            return View("Index");
        }

        public ActionResult AssignRole()
        {
            ViewBag.Roles = db.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(FormCollection form)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            string userName = form["txtUserName"];
            string roleName = form["RoleName"];
            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            userManager.AddToRole(user.Id, roleName);

            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userdetail = this.db.UserDetails.FirstOrDefault(p => p.Id == id);
            if (userdetail == null)
            {
                return HttpNotFound();
            }

            return View(userdetail);

        }

        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,FirstName,LastName,Address,City")]UserDetail model)
        {            
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View();
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userdetail = this.db.UserDetails.FirstOrDefault(p => p.Id == id);
            if (userdetail == null)
            {
                return HttpNotFound();
            }

            return View(userdetail);
        }


    }
}