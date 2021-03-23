using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Syntra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.DB
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<DanceSchoolDbContext>>();
            using (var context = new DanceSchoolDbContext(dbContextOptions))
            {
                if (context.Teachers.Count() == 0)
                {
                    var teacher1 = new Teacher();
                    teacher1.FirstName = "Guy";
                    teacher1.LastName = "CryptoDev";
                    teacher1.Salary = 10000;
                    var teacher2 = new Teacher();
                    teacher2.FirstName = "Bart";
                    teacher2.LastName = "Van Gucht";
                    teacher2.Salary = 1000000;
                    context.Add(teacher1);
                    context.Add(teacher2);
                    context.SaveChanges();
                }
                if (context.Locations.Count() == 0)
                {
                    var location1 = new Location();
                    location1.Street = "vuurstraat";
                    location1.StreetNumber = "yup";
                    location1.City = "Mechelen";
                    var location2 = new Location();
                    location2.Street = "Sesamestreet";
                    location2.StreetNumber = "123";
                    location2.City = "New York City";
                    context.Add(location1);
                    context.Add(location2);
                    context.SaveChanges();
                }
                if (context.Courses.Count() == 0)
                {
                    var course1 = new Course();
                    course1.Name = "randomNaam";
                    //course1.DateTime;
                    course1.TeacherId = 1;
                    course1.LocationId = 2;
                    var course2 = new Course();
                    course1.Name = "EenAndereNaam";
                    //course1.DateTime;
                    course1.TeacherId = 2;
                    course1.LocationId = 1;

                    context.Add(course1);
                    context.Add(course2);
                    context.SaveChanges();
                }
            }
        }
    }
}
