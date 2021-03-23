using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Syntra.Models;
using Syntra.MVCAdvanced.DB;
using Syntra.MVCAdvanced.Services.Interfaces;
using Syntra.MVCAdvanced.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseDbService _courseDbService;
        private readonly IMapper _mapper;
        private readonly DanceSchoolDbContext _context;

        public CourseController(ICourseDbService courseDbService, IMapper mapper, DanceSchoolDbContext context)
        {
            this._courseDbService = courseDbService;
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _courseDbService.GetListAsync();
            var courseToCourseDetailsVW = _mapper.Map<List<CourseDetailsVM>>(courses);
            //var teacherList = (_courseDbService.TeacherGet());
            //ViewBag.ListofTeachers = teacherList;
            return View(courseToCourseDetailsVW);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var courseFromDb = await _courseDbService.GetOneAsync(id);
            if(courseFromDb == null)
            {
                return NotFound();
            }
            var courseVM = _mapper.Map<CourseDetailsVM>(courseFromDb);
            return View(courseVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var courseFromDb = await _courseDbService.GetOneAsync(id);
            if (courseFromDb == null)
            {
                return NotFound();
            }
            var courseVM = _mapper.Map<CourseDetailsVM>(courseFromDb);
            return View(courseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,DateTime,TeacherId,LocationId")] CourseDetailsVM courseVM)
        {
            if (ModelState.IsValid)
            {
                var courseToUpdate = _mapper.Map<Course>(courseVM);
                var updatedCourse = await _courseDbService.UpdateAsync(courseToUpdate);
                var courseVMToReturn = _mapper.Map<CourseDetailsVM>(updatedCourse);
                return View(courseVMToReturn);
            }
            return View(courseVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var courseFromDb = await _courseDbService.GetOneAsync(id);
            if (courseFromDb == null)
            {
                return NotFound();
            }
            var courseVM = _mapper.Map<CourseDetailsVM>(courseFromDb);
            return View(courseVM);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseDbService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            var listOfDropDowns = (_courseDbService.TeacherGet());
            ViewBag.ListofTeachers = listOfDropDowns[0];
            ViewBag.ListofLocations = listOfDropDowns[1];
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateTime,TeacherId,LocationId")] CourseDetailsVM courseVM)
        {
            var toCourse = _mapper.Map<Course>(courseVM);
            var courseToCreate = await _courseDbService.CreateAsync(toCourse);
            var courseToCourseVM = _mapper.Map<CourseDetailsVM>(courseToCreate);
            return View(courseToCourseVM);
        }
    }
}
