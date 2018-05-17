using System;

namespace web_api.Model
{
    public class User {
        public int ID { get; set; }
        public string Email { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] SaltPassword { get; set; }
    }
}