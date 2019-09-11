using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRSDbfirst.Models
{
    public partial class Vendors
    {
        public Vendors()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        [Display(Name = "Company Code")]
        public string Code { get; set; }
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
