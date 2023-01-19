using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Domain.Entities
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string JsonValue { get; set; }
        public bool IsDeleted { get; set; }
    }
}
