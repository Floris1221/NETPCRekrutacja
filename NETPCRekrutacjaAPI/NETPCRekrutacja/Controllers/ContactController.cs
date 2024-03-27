using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAuthYtAPI.Context;
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
        public async Task<ActionResult<Contact>> GetAllContacts()
        {
            var contacts = await _authContext.Contacts
                .Include(entity => entity.Category)
                .ToListAsync();
            return Ok(contacts);
        }

        [HttpPost("addContact")]
        public async Task<ActionResult<Contact>> AddContact([FromBody] ContactDto entity)
        {
            if (entity == null)
                return BadRequest();

            Contact contact = new Contact(
                entity.FirstName,
                entity.LastName,
                entity.Email,
                entity.Phone,
                entity.Category
                );

            await _authContext.AddAsync(contact);
            await _authContext.SaveChangesAsync();

           

            return Ok(new
            {
                Status = 201,
                Message = "Contact Added!"
            });
        }

    }
}

