using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth.Models.WebModel
{
    public  class Registration
    {
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Username cannot be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password cannot be empty")]
        public string Password { get; set; }    
    }
}
