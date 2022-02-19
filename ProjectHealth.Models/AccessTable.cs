using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth.Models
{
    public class AccessTable
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Passwordhash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
