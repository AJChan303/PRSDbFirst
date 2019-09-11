using System;
using System.Collections.Generic;

namespace PRSDbfirst.Models
{
    public partial class RequestLines
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Products Product { get; set; }
        public virtual Requests Request { get; set; }
    }
}
