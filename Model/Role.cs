using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api.Model
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
