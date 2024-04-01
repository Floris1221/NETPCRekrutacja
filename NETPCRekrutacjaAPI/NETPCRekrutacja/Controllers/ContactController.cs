using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Helpers;
using AngularAuthYtAPI.Models;
using AngularAuthYtAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularAuthYtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public ContactController(AppDbContext context)
        {
            _authContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<Contact>> GetAllContacts() //should be DTO
        {
            var contacts = await _authContext.Contacts
                .Include(entity => entity.Category)
                .Include(entity => entity.Subcategory)
                .ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            var contact = await _authContext.Contacts
                .Include(entity => entity.Category)
                .Include(entity => entity.Subcategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                return NotFound(new { Message = "Contact not found." });
            }

            var contactDto = new ContactDto(contact.Id,
                contact.FirstName,
                contact.LastName,
                contact.Email,
                contact.Phone,
                contact.CategoryId,
                contact.SubcategoryId,
                contact.Subcategory == null || contact.Subcategory.Base ? null : contact.Subcategory.Name,
                contact.Password, //shouldnt be like that
                contact.DateOfBirth
                );

            return Ok(contactDto);
        }


        [HttpPost("addContact")]
        [Authorize]
        public async Task<ActionResult<ContactDto>> AddContact([FromBody] ContactDto entity)
        {
            if (entity == null)
                return BadRequest();

            if (!string.IsNullOrEmpty(entity.Newsubcategory)){
                var subcategory = new SubCategory(entity.Newsubcategory);

                await _authContext.AddAsync(subcategory);
                await _authContext.SaveChangesAsync();
                entity.Subcategory = subcategory.Id;
            }

            Contact contact = new Contact(
                entity.FirstName,
                entity.LastName,
                entity.Email,
                entity.Phone,
                entity.Category,
                entity.Subcategory,
                entity.Password,
                entity.DateOfBirth
                );

            await _authContext.AddAsync(contact);
            await _authContext.SaveChangesAsync();

           

            return Ok(new
            {
                Status = 201,
                Message = "Contact Added!"
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactDto entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            var contact = await _authContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound(new { Message = "Contact not found." });
            }

            if (!string.IsNullOrEmpty(entity.Newsubcategory)
                || (contact.Subcategory != null && entity.Newsubcategory.Trim() != contact.Subcategory.Name))
            {
                var subcategory = new SubCategory(entity.Newsubcategory);

                await _authContext.AddAsync(subcategory);
                await _authContext.SaveChangesAsync();
                entity.Subcategory = subcategory.Id;
            }


            contact.FirstName = entity.FirstName;
            contact.LastName = entity.LastName;
            contact.Email = entity.Email;
            contact.Phone = entity.Phone;
            contact.CategoryId = entity.Category;
            contact.SubcategoryId = entity.Subcategory;
            contact.Password = entity.Password;
            contact.DateOfBirth = DateTime.ParseExact(entity.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            try
            {
                _authContext.Contacts.Update(contact);
                await _authContext.SaveChangesAsync();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            return Ok(new { Message = "Contact updated successfully." });
        }


        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmailUniqueness(string email, string? contactId = null)
        {
            var isUnique = await _authContext.Contacts
                .Where(contact => contact.Email == email && (contactId == null || contact.Id.ToString() != contactId))
                .AnyAsync() == false;

            return Ok(isUnique);
        }


    }
}

