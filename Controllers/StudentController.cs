using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrainingApi.Database;
using TrainingApi.Database.Entities;
using System.Linq;

namespace TrainingApi.Controllers
{
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
        public ActionResult<Student> AddStudent([FromBody]Student student)
        {
            student.StudentId = InMemoryDatabase.Students.Max(student => student.StudentId) + 1;
            InMemoryDatabase.Students.Add(student);

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        [HttpPut]
        public ActionResult<Student> ChangeStudent([FromBody]Student studentChange)
        {
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