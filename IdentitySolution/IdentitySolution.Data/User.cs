using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentitySolution.Data
{
    public class User : IdentityUser
    {
        public User() { }
        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
       
    }
}
