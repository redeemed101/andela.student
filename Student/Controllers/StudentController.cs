using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.Include(s  => s.StudentCourses).ThenInclude(s => s.Course).ToListAsync();
            return Ok(new { students});
        }

    }
}
