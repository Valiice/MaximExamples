using Syntra.Models;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Services.Interfaces
{
    public interface ITeacherDbService
    {
        Task<Teacher> GetOneAsync(int id);
        Task<Teacher> UpdateAsync(Teacher teacherToSave);
    }
}