using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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


        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public Contact() { }

        public Contact(string FirstName,
            string LastName,
            string Email,
            string Phone,
            int CategoryId,
            int? Subcategory,
            string Password,
            string DateOfBirth)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            this.CategoryId = CategoryId;
            this.Phone = Phone;
            this.SubcategoryId = Subcategory;
            this.DateOfBirth = DateTime.ParseExact(DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

    }
}

