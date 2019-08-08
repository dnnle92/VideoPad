using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace VideoSIte.Models
{
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
        public virtual ICollection<Product> Products { get; set; }

        public class Mapping : EntityTypeConfiguration<Category>
        {
            public Mapping()
            {
                HasOptional(m => m.Parent).WithMany(m => m.Children);
            }
        }
    }
}