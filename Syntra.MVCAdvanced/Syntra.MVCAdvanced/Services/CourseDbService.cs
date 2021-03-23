using Microsoft.EntityFrameworkCore;
using Syntra.Models;
using Syntra.MVCAdvanced.DB;
using Syntra.MVCAdvanced.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Syntra.MVCAdvanced.Services
{
    public class CourseDbService : ICourseDbService
    {
        private readonly DanceSchoolDbContext _context;

        public CourseDbService(DanceSchoolDbContext context)
        {
            this._context = context;
        }
        public async Task<Course> GetOneAsync(int? id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Course> UpdateAsync(Course courseToUpdate)
        {
            _context.Courses.Update(courseToUpdate);
            await _context.SaveChangesAsync();
            return courseToUpdate;
        }
        public async Task<List<Course>> GetListAsync()
        {
            var allCourses = await _context.Courses.ToListAsync();
            if (allCourses.Count() != 0)
            {
                return allCourses;
            }
            else
                throw new KeyNotFoundException("The courses don't exist");
        }
        public async Task<Course> CreateAsync(Course courseToCreate)
        {
            _context.Courses.Add(courseToCreate);
            await _context.SaveChangesAsync();
            return courseToCreate;
        }
        public async Task DeleteAsync(int Id)
        {
            var courseToDelete = await _context.Courses.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Courses.Remove(courseToDelete);
            await _context.SaveChangesAsync();
        }
        public List<List<SelectListItem>> TeacherGet()
        {
            var itemsForDropDown = new List<List<SelectListItem>>();
            var teacherList = (from Teacher in _context.Teachers
                               select new SelectListItem()
                               {
                                   Text = $"{Teacher.FirstName} {Teacher.LastName}",
                                   Value = Teacher.Id.ToString(),
                               }).ToList();
            teacherList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            var locationList = (from Location in _context.Locations
                               select new SelectListItem()
                               {
                                   Text = $"{Location.Street} {Location.StreetNumber} {Location.City}",
                                   Value = Location.Id.ToString(),
                               }).ToList();
            locationList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            itemsForDropDown.Add(teacherList);
            itemsForDropDown.Add(locationList);
            return itemsForDropDown;
        }
    }
}
