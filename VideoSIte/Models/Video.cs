using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoSIte.Models
{
    public class Video
    {
        public Video()
        {
            this.Categories = new HashSet<Category>();
        }
        [Key]
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }
        public string VideoUrlId { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}