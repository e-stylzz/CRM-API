using System;
namespace CRMAPI.WebApi.DTO
{
    public class ContactPutDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
