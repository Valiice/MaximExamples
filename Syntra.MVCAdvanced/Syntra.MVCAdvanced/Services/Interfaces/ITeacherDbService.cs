using Syntra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Services.Interfaces
{
    public interface ITeacherDbService
    {
        Task<Teacher> GetOneAsync(int? id);
        Task<Teacher> UpdateAsync(Teacher teacherToSave);
        Task<List<Teacher>> GetListAsync();
        Task<Teacher> CreateAsync(Teacher teacherToCreate);
        Task DeleteAsync(int Id);
    }
}