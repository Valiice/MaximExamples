using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Syntra.Models;
using Syntra.MVCAdvanced.Services.Interfaces;
using Syntra.MVCAdvanced.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationDbService _locationDbService;
        private readonly IMapper _mapper;

        public LocationController(ILocationDbService locationDbService, IMapper mapper)
        {
            this._locationDbService = locationDbService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var locations = await _locationDbService.GetListAsync();
            var locationToLocationDetailsVW = _mapper.Map<List<LocationDetailsVM>>(locations);
            return View(locationToLocationDetailsVW);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var locationFromDb = await _locationDbService.GetOneAsync(id);
            if (locationFromDb == null)
            {
                return NotFound();
            }
            var locationVM = _mapper.Map<LocationDetailsVM>(locationFromDb);
            return View(locationVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var locationFromDb = await _locationDbService.GetOneAsync(id);
            if (locationFromDb == null)
            {
                return NotFound();
            }
            var locationVM = _mapper.Map<LocationDetailsVM>(locationFromDb);
            return View(locationVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Street,StreetNumber,City")] LocationDetailsVM locationVM)
        {
            if (ModelState.IsValid)
            {
                var locationUpdate = _mapper.Map<Location>(locationVM);
                var updatedLocation = await _locationDbService.UpdateAsync(locationUpdate);
                var locationVMToReturn = _mapper.Map<LocationDetailsVM>(updatedLocation);
                return View(locationVMToReturn);
            }
            return View(locationVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var locationFromDb = await _locationDbService.GetOneAsync(id);
            if (locationFromDb == null)
            {
                return NotFound();
            }
            var locationVM = _mapper.Map<LocationDetailsVM>(locationFromDb);
            return View(locationVM);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _locationDbService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Street,StreetNumber,City")] LocationDetailsVM locationVM)
        {
            var toLocation = _mapper.Map<Location>(locationVM);
            var locationToCreate = await _locationDbService.CreateAsync(toLocation);
            var locationToLocationVM = _mapper.Map<LocationDetailsVM>(locationToCreate);
            return View(locationToLocationVM);
        }
    }
}
