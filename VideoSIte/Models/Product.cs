using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoSIte.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }
        public string ProductImage { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}