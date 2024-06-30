using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Student>> FilterBy(string? name, string? surname, int? age)
        {
            if(name is null)
            {
                var data = await _context.Students.Where(m => m.Age==age && m.SurName.Contains(surname.Trim())).ToListAsync();
                return data;
            }
            else if(surname is null)
            {
                var data = await _context.Students.Where(m => m.Age == age && m.SurName.Contains(surname.Trim())).ToListAsync();
                return data;
            }
            else if (age is null)
            {
                var data = await _context.Students.Where(m => m.Name.Contains(name.Trim()) && m.SurName.Contains(surname.Trim())).ToListAsync();
                return data;
            }
            else
            {
                var data = await _context.Students.Where(m => m.Name.Contains(name.Trim())&& m.Age == age && m.SurName.Contains(surname.Trim())).ToListAsync();
                return data;
            }
            
        }

        public async Task<IEnumerable<Student>> GetAllWithAsync()
        {
            var students = await _context.Students
            .Include(s => s.GroupStudents)
            .ThenInclude(gs => gs.Group)
            .ToListAsync();


            return students;
        }

        public async Task<Student> GetByIdWithAsync( int? id)
        {
            if (id == null) { throw new ArgumentNullException(nameof(id)); }
            var data = await _context.Students.AsNoTracking().Include(m=>m.GroupStudents).ThenInclude(m=>m.Group).FirstOrDefaultAsync(m => m.Id==id);  
            return data;
        }

        public async Task<IEnumerable<Student>> GetByNameOrSurname(string nameOrSurname)
        {
            var data = await _context.Students.Where(m => m.Name.Trim().Contains(nameOrSurname.Trim()) || m.SurName.Contains(nameOrSurname.Trim())).ToListAsync();
            return data;
        }
    }
}
