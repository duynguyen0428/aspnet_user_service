using System;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace web_api.Model
{
    public class User {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] SaltPassword { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public Role Role { get; set; }
    }
}