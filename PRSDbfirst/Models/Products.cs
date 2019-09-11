using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRSDbfirst.Models
{
    public partial class Products
    {
        public Products()
        {
            RequestLines = new HashSet<RequestLines>();
        }

        public int Id { get; set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
        public int VendorId { get; set; }

        public virtual Vendors Vendor { get; set; }
        public virtual ICollection<RequestLines> RequestLines { get; set; }
    }
}
