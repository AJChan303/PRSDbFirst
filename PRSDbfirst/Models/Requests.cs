using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace PRSDbfirst.Models
{
    public partial class Requests {
        public Requests() {
            //RequestLines = new HashSet<RequestLines>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get ; set; }
        public int UserId { get; set; }
        public string RejectionReason { get; set; }
        public DateTime? DateRequested {
            get {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;



        public virtual Users User { get; set; }
        //public virtual ICollection<RequestLines> RequestLines { get; set; }
    }
}
