using System;
using System.Collections.Generic;

namespace PRSDbfirst.Models
{
    public partial class Users
    {
        public Users()
        {
            Requests = new HashSet<Requests>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }
    }
}
