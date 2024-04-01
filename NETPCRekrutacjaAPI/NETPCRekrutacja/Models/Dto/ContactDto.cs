using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthYtAPI.Models.Dto
{
	public class ContactDto
	{
        public int? Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Category { get; set; }

        public int? Subcategory { get; set; }

        public string? Newsubcategory { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public String DateOfBirth { get; set; }


        public ContactDto() { }

        public ContactDto(int Id,
            string FirstName,
            string LastName,
            string Email,
            string Phone,
            int CategoryId,
            int? Subcategory,
            string? Newsubcategory,
            string Password,
            DateTime DateOfBirth)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Category = CategoryId;
            this.Phone = Phone;
            this.Subcategory = Subcategory;
            this.Newsubcategory = Newsubcategory;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth.ToString("yyyy-MM-dd");

        }
    }

}

