using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<Teacher> GetByIdWithAsync(int? id)
        {
            if (id == null) { throw new ArgumentNullException(nameof(id)); }
            var data = await _context.Teachers.AsNoTracking().Include(s => s.GroupTeachers)
            .ThenInclude(gs => gs.Group).FirstOrDefaultAsync(m => m.Id == id);
            return data;
        }
        public async Task<IEnumerable<Teacher>> GetAllWithAsync()
        {
            var teachers = await _context.Teachers
            .Include(s => s.GroupTeachers)
            .ThenInclude(gs => gs.Group)
            .ToListAsync();


            return teachers;
        }

        public async Task<IEnumerable<Teacher>> GetByNameOrSurname(string nameOrSurname)
        {
            var data = await _context.Teachers.Where(m => m.Name.Trim().Contains(nameOrSurname.Trim())||m.Surname.Contains(nameOrSurname.Trim())).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Teacher>> SortByName(string message)
        {
            if (message == "asc")
            {
                var data = await _context.Teachers.OrderBy(m => m.Name).ToListAsync();
                return data;
            }
            else
            {
                var data = await _context.Teachers.OrderByDescending(m => m.Name).ToListAsync();

                return data;
            }
        }

        public async Task<IEnumerable<Teacher>> SortByAge(string message)
        {
            if (message == "asc")
            {
                var data = await _context.Teachers.OrderBy(m => m.Age).ToListAsync();
                return data;
            }
            else
            {
                var data = await _context.Teachers.OrderByDescending(m => m.Age).ToListAsync();

                return data;
            }
        }

        public async Task<IEnumerable<Teacher>> SortBySalary(string message)
        {
            if (message == "asc")
            {
                var data = await _context.Teachers.OrderBy(m => m.Salary).ToListAsync();
                return data;
            }
            else
            {
                var data = await _context.Teachers.OrderByDescending(m => m.Salary).ToListAsync();

                return data;
            }
        }
    }
}
