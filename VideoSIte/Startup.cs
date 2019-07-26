using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using VideoSIte.Models;

[assembly: OwinStartupAttribute(typeof(VideoSIte.Startup))]
namespace VideoSIte
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                //This creates a admin role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Create Default Users
                var user = new ApplicationUser();
                user.UserName = "dl@domain.com";
                user.Email = "dl@domain.com";
                string userPwd = "Pwd@12345";

                var newuser = userManager.Create(user, userPwd);

                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Premium"))
            {
                var role = new IdentityRole();
                role.Name = "Premium";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Disabled"))
            {
                var role = new IdentityRole();
                role.Name = "Disabled";
                roleManager.Create(role);
            }
        }
    }
}
