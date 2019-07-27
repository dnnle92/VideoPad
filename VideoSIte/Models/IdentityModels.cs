using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VideoSIte.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class UserDetail
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }

    public class Video
    {
        public Video()
        {
            this.Categories = new HashSet<Category>();
        }
        [Key]
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        public System.DateTime DateAdded { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }

    public class Category
    {
        public Category()
        {
            this.Videos = new HashSet<Video>();
        }
        [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<Video> Videos { get; set; }

        public class Mapping : EntityTypeConfiguration<Category>
        {
            public Mapping()
            {
                HasOptional(m => m.Parent).WithMany(m => m.Children);
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Category.Mapping());
            modelBuilder.Entity<Video>()
                .HasMany<Category>(v => v.Categories)
                .WithMany(c => c.Videos)
                .Map(vc =>
                    {
                        vc.MapLeftKey("VideoId");
                        vc.MapRightKey("VideoCatId");
                        vc.ToTable("VideoCategory");
                    });

            base.OnModelCreating(modelBuilder);

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}