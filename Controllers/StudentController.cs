using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrainingApi.Database;
using TrainingApi.Database.Entities;
using System.Linq;

namespace TrainingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public List<Student> GetStudents()
        {
            return InMemoryDatabase.Students;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = InMemoryDatabase.Students.SingleOrDefault(student => student.StudentId == id);

            if (student != null)
            {
                return student;
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Student> AddStudent([FromBody]Student student)
        {
            if (student == null) return BadRequest();

            student.StudentId = InMemoryDatabase.Students.Max(student => student.StudentId) + 1;
            InMemoryDatabase.Students.Add(student);

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult ChangeStudent([FromBody]Student studentChange)
        {
            if (studentChange == null) return BadRequest();

            var student = InMemoryDatabase.Students.SingleOrDefault(student => student.StudentId == studentChange.StudentId);

            if (student != null)
            {
                InMemoryDatabase.Students.Remove(student);
                InMemoryDatabase.Students.Add(studentChange);
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult DeleteStudent(int id)
        {
            var student = InMemoryDatabase.Students.SingleOrDefault(student => student.StudentId == id);

            if (student != null)
            {
                InMemoryDatabase.Students.Remove(student);
                return NoContent();
            }

            return NotFound();
        }
    }
}