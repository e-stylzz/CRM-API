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



        /// <summary>
        ///  Returns all of your contacts
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /Contact
        /// 
        /// </remarks>
        /// <returns>All Contacts</returns>
        /// <response code="200">Returns all contacts</response>
        /// <response code="401">If you are not authorized</response>
        [HttpGet]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(401)]
        public IActionResult GetAll()
        {
            var contact = contactService.GetContacts();
            var viewModel = mapper.Map<IEnumerable<ContactDto>>(contact);
            return Ok(viewModel);
        }


        /// <summary>
        ///  Returns a specific contact by Id
        /// </summary>
        /// <returns>The all.</returns>
        /// <response code="200">Returns specific contact contacts</response>
        /// <response code="401">If you are not authorized</response>
        [HttpGet("{id}", Name = "GetContact")]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(401)]
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
