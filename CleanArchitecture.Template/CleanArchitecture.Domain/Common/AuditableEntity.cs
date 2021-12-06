using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Common
{
    public class AuditableEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set;}
        public string? LastModifieddBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
