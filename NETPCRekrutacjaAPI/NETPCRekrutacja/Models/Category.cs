using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthYtAPI.Models
{
	public class Category
	{
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        public List<SubCategory> Subcategories { get; set; }
    }
}

