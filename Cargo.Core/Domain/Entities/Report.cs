using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo.Core.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int TrackingId { get; set; }
        public string ShopName { get; set; }
        public short InvoicePrice { get; set; }
        public short ProductCount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime CreationDateTime { get; set; }       
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
