using Microsoft.EntityFrameworkCore;
using Syntra.Models;
using Syntra.MVCAdvanced.DB;
using Syntra.MVCAdvanced.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Services
{
    public class TeacherDbService : ITeacherDbService
    {
        private readonly DanceSchoolDbContext _context;
        public TeacherDbService(DanceSchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Teacher> GetOneAsync(int? id)
        {
            return await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Teacher> UpdateAsync(Teacher teacherToSave)
        {
            _context.Teachers.Update(teacherToSave);
            await _context.SaveChangesAsync();
            return teacherToSave;
        }
        public async Task<List<Teacher>> GetListAsync()
        {
            var allTeachers = await _context.Teachers.ToListAsync();
            if (allTeachers.Count() != 0)
            {
                return allTeachers;
            }
            else
                throw new KeyNotFoundException("The teacher doesn't exist");
        }
        public async Task<Teacher> CreateAsync(Teacher teacherToCreate)
        {
            _context.Teachers.Add(teacherToCreate);
            await _context.SaveChangesAsync();
            return teacherToCreate;
        }
        public async Task DeleteAsync(int Id)
        {
            var teacherToDelete = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Teachers.Remove(teacherToDelete);
            await _context.SaveChangesAsync();
            //return teacherToDelete;
        }
    }
}
