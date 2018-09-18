using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CRMAPI.WebApi.DTO;
using CRMAPI.Service;
using CRMAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMAPI.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {

        private IMapper mapper;
        private readonly IContactService contactService;
        public ContactsController(IMapper mapper,
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
        ///     GET /Contacts
        /// 
        /// </remarks>
        /// <returns>All Contacts</returns>
        /// <response code="200">Returns all contacts</response>
        /// <response code="401">If you are not authorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(401)]
        public IActionResult GetAll()
        {
            var contacts = contactService.GetContacts();
            var viewModel = mapper.Map<IEnumerable<ContactDto>>(contacts);
            return Ok(viewModel);
        }


        /// <summary>
        ///  Returns a specific contact by Id
        /// </summary>
        /// <returns>The all.</returns>
        /// <response code="200">Returns specific contact contacts</response>
        /// <response code="401">If you are not authorized</response>
        [HttpGet("{id}", Name = "GetContact")]
        [Authorize]
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

        /// <summary>
        ///  Creates a contact
        /// </summary>
        /// <returns>The created contact</returns>
        /// <response code="201">Returns specific contact contacts</response>
        /// <response code="400">If the data is invalid</response>
        /// <response code="401">If you are not authorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult CreateContact([FromBody]ContactCreateDto contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var finalContact = mapper.Map<Contact>(contact);
            contactService.CreateContact(finalContact);

            var createdContact = mapper.Map<ContactDto>(finalContact);

            return CreatedAtRoute("GetContact", new { id = createdContact.Id }, createdContact);
        }

        /// <summary>
        ///  Updates a contact
        /// </summary>
        /// <remarks>
        /// Body must contain ID currently
        /// </remarks>
        /// <returns>The updated contact</returns>
        /// <response code="200">Returns the updated contact</response>
        /// <response code="400">If the data is invalid</response>
        /// <response code="401">If you are not authorized</response>
        /// <response code="500">Server Exception</response>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult UpdateContact(Guid id, [FromBody]ContactPutDto contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var originalContact = contactService.GetContact(id);

            if (originalContact == null)
            {
                return NotFound();
            }

            if (originalContact.Id != contact.Id)
            {
                return BadRequest();
            }

            mapper.Map(contact, originalContact);

            contactService.UpdateContact(originalContact);
            return NoContent();
        }

        /// <summary>
        ///  Deletes a contact
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="200">Returns the updated contact</response>
        /// <response code="400">If the data is invalid</response>
        /// <response code="401">If you are not authorized</response>
        /// <response code="500">Server Exception</response>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = contactService.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            contactService.DeleteContact(id);
            return NoContent();
        }

    }
}
