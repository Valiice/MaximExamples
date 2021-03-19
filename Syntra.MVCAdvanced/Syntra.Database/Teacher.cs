using System;
using System.Collections.Generic;

namespace Syntra.Models
{
    public class Teacher
    {
        //public Teacher()
        //{
        //    Courses = new List<Course>();
        //}
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public List<Course> Courses { get; set; }

    }
}
