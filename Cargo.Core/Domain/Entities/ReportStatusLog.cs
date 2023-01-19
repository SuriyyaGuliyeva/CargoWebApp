using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Core.Domain.Entities
{
    public class ReportStatusLog
    {
        [Key]
        public int Id { get; set; }
        public string NextStatus { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}