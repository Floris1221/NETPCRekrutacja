using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularAuthYtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public CategoryController(AppDbContext context)
        {
            _authContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> GetAllCategories()
        {
            return Ok(await _authContext.Categories.ToListAsync());
        }


        [HttpGet("subcategory")]
        public async Task<ActionResult<SubCategory>> GetAllSubcategories()
        {
            return Ok(await _authContext.Subcategories
                .Where(subcategory => subcategory.Base == true)
                .ToListAsync());
        }
    }
}

