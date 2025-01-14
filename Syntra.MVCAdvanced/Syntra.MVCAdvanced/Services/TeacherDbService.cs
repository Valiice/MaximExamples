﻿using Microsoft.EntityFrameworkCore;
using Syntra.Models;
using Syntra.MVCAdvanced.DB;
using Syntra.MVCAdvanced.Services.Interfaces;
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
        public async Task<Teacher> GetOneAsync(int id)
        {
            return await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Teacher> UpdateAsync(Teacher teacherToSave)
        {
            _context.Teachers.Update(teacherToSave);
            await _context.SaveChangesAsync();
            return teacherToSave;
        }
    }
}
