using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthYtAPI.Models
{
	public class SubCategory
	{
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Base { get; set; } = false;

        public SubCategory() { }

        public SubCategory(string Name)
        {
            this.Name = Name;
        }
    }
}

