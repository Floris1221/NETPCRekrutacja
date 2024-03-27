using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthYtAPI.Models
{
	public class Contact
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        
        public string Password { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int? SubcategoryId { get; set; }
        public SubCategory Subcategory { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

       
        public DateTime DateOfBirth { get; set; }

        public Contact(string FirstName, string LastName, string Email, string Phone, int CategoryId)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = "123";
            this.CategoryId = CategoryId;
            this.Phone = Phone;
        }

    }
}

