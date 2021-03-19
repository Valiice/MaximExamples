using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TeacherController : Controller
    {
        private readonly ITeacherDbService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherDbService teacherDbService, IMapper mapper)
        {
            _teacherService = teacherDbService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetListAsync();
            var teacherToTeacherDetailsVW = _mapper.Map<List<TeacherDetailsVM>>(teachers);
            return View(teacherToTeacherDetailsVW);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var teacherFromDb = await _teacherService.GetOneAsync(id);
            if(teacherFromDb == null)
            {
                return NotFound();
            }
            var teacherVM =  _mapper.Map<TeacherDetailsVM>(teacherFromDb);
            return View(teacherVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var teacherFromDb = await _teacherService.GetOneAsync(id);
            if (teacherFromDb == null)
            {
                return NotFound();
            }
            var teacherVM = _mapper.Map<TeacherDetailsVM>(teacherFromDb);
            return View(teacherVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,FirstName,LastName,Salary")] TeacherDetailsVM teacherVM)
        {

            if (ModelState.IsValid) //is het valid?
            {
                var teacherToUpdate = _mapper.Map<Teacher>(teacherVM); // maak van de vm een teacher object
                var updatedTeacher = await _teacherService.UpdateAsync(teacherToUpdate); // geef het teacher object mee aan de update functie
                var teacherVMToReturn = _mapper.Map<TeacherDetailsVM>(updatedTeacher); // map de geupdate teacher terug naar een VM
                return View(teacherVMToReturn); // return de view  met de VM
            }
            return View(teacherVM); // De view was niet valid, maak opnieuw de view met de invalid teacherVM
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var teacherFromDb = await _teacherService.GetOneAsync(id);
            if (teacherFromDb == null)
            {
                return NotFound();
            }
            var teacherVM = _mapper.Map<TeacherDetailsVM>(teacherFromDb);
            return View(teacherVM);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.DeleteAsync(id);
            //var teacherToVM = _mapper.Map<TeacherDetailsVM>(teacherToDelete);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Salary")] TeacherDetailsVM teacherVM)
        {
            var toTeacher = _mapper.Map<Teacher>(teacherVM);
            var teacherToCreate = await _teacherService.CreateAsync(toTeacher);
            var teacherToTeacherVM = _mapper.Map<TeacherDetailsVM>(teacherToCreate);
            return View(teacherToTeacherVM);
        }
    }
}
