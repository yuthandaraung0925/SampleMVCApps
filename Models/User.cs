using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

#nullable disable

namespace SampleMVCApps.Models
{
    public class UserValidator
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "UserName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Joining Date")]
        public decimal JoiningDate { get; set; }

        private class RequiredAttribute : Attribute
        {
        }
    }

    [ModelMetadataType(typeof(UserValidator))]
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
