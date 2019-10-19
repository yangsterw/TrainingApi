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
    public class CourseController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Course>> GetCourses()
        {
            return Ok(InMemoryDatabase.Courses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Course> GetCourse(int id)
        {
            var course = InMemoryDatabase.Courses.SingleOrDefault(course => course.CourseId == id);

            if (course != null)
            {
                return course;
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Course> AddCourse([FromBody]Course course)
        {
            course.CourseId = InMemoryDatabase.Courses.Max(course => course.CourseId) + 1;
            InMemoryDatabase.Courses.Add(course);

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult ChangeCourse([FromBody]Course courseChange)
        {
            var course = InMemoryDatabase.Courses.SingleOrDefault(course => course.CourseId == courseChange.CourseId);

            if (course != null)
            {
                InMemoryDatabase.Courses.Remove(course);
                InMemoryDatabase.Courses.Add(courseChange);
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCourse(int id)
        {
            var course = InMemoryDatabase.Courses.SingleOrDefault(course => course.CourseId == id);

            if (course != null)
            {
                InMemoryDatabase.Courses.Remove(course);
                return NoContent();
            }

            return NotFound();
        }
    }
}