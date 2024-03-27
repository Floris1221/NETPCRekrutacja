using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthYtAPI.Models.Dto
{
	public class ContactDto
	{
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Category { get; set; }

        public string Phone { get; set; }
    }
}

