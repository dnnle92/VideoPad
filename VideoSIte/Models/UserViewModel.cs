using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;


namespace VideoSIte.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
    public class EditViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}