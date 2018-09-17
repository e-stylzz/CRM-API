using System;
namespace CRMAPI.WebApi.DTO
{
    public class ContactDto
    {
        public Guid Id { get; set;  }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
