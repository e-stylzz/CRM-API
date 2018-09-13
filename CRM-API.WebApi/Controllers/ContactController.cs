using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CRMAPI.WebApi.DTO;
using CRMAPI.Service;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMAPI.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {

        private IMapper mapper;
        private readonly IContactService contactService;
        public ContactController(IMapper mapper,
                                                    IContactService contactService)
        {
            this.mapper = mapper;
            this.contactService = contactService;
        }



        // GET: api/contact
        [HttpGet]
        [ProducesResponseType(typeof(ContactDto), 200)]
        public IActionResult GetAll()
        {
            var contact = contactService.GetContacts();
            var viewModel = mapper.Map<IEnumerable<ContactDto>>(contact);
            return Ok(viewModel);
        }


        // GET api/contact/5
        [HttpGet("{id}", Name = "GetContact")]
        [ProducesResponseType(typeof(ContactDto), 200)]
        public IActionResult Get(Guid id)
        {
            var contact = contactService.GetContact(id);

            if (contact == null){
                return NotFound();
            }

            var viewModel = mapper.Map<ContactDto>(contact);
            return Ok(viewModel);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
