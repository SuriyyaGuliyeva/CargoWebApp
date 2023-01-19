using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo.Core.Domain.Entities
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public int TrackingId { get; set; }
        public string ShopName { get; set; }
        public short InvoicePrice { get; set; }
        public short ProductCount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime CreationDateTime { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
