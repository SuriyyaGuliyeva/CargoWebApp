using System;

namespace Cargo.Core.Domain.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
