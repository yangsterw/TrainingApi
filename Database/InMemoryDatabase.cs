using System.Collections.Generic;
using TrainingApi.Database.Entities;

namespace TrainingApi.Database
{
    public static class InMemoryDatabase
    {
        static InMemoryDatabase()
        {
            Initialize();
        }

        private static void Initialize()
        {
            Students = new List<Student>
            {
                new Student
                {
                    StudentId = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Year = "Senior"
                },
                new Student
                {
                    StudentId = 2,
                    FirstName = "Mary",
                    LastName = "Jones",
                    Year = "Senior"
                }
            };

            Courses = new List<Course>
            {
                new Course
                {
                    CourseId = 1,
                    Title = "Cloud Computing",
                    Level = "101"
                },

                new Course
                {
                    CourseId = 2,
                    Title = "Web Design and Development",
                    Level = "101"
                }
            };

            Enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 1
                },
                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 2
                },
                new Enrollment
                {
                    StudentId = 2,
                    CourseId = 1
                },
                new Enrollment
                {
                    StudentId = 2,
                    CourseId = 2
                },
            };
        }

        public static List<Student> Students;
        public static List<Course> Courses;
        public static List<Enrollment> Enrollments;
    }

}
