using System;
using System.ComponentModel.DataAnnotations;

namespace CRMAPI.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        [MaxLength(100)]
        public string ModifiedBy { get; set; }
    }
}
