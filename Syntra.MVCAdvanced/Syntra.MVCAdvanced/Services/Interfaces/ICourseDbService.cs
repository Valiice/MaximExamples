using Microsoft.AspNetCore.Mvc.Rendering;
using Syntra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Services.Interfaces
{
    public interface ICourseDbService
    {
        Task<Course> CreateAsync(Course courseToCreate);
        Task DeleteAsync(int Id);
        Task<List<Course>> GetListAsync();
        Task<Course> GetOneAsync(int? id);
        Task<Course> UpdateAsync(Course courseToUpdate);
        List<List<SelectListItem>> TeacherGet();
    }
}