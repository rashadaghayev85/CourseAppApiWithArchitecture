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
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Group>> GetAllWithAsync()
        {
            var data= await _context.Groups.AsNoTracking().Include(m => m.GroupStudents).Include(m=>m.Education).Include(m=>m.Room).ToListAsync();
           return data; 
        }

        public async Task<List<Group>> GetByIdWithAsync(List<int>? id)
        {

            if (id == null) throw new ArgumentNullException(nameof(id));
            List<Group> groups = new();
            foreach (var item in id)
            {
                if (!await _context.Groups.AnyAsync(g => g.Id == item))
                {
                    throw new NullReferenceException(nameof(item));

                }
                else
                {
                    groups.Add(await _context.Groups.AsNoTracking().FirstOrDefaultAsync(m => m.Id == item));
                }
            }


            return groups;
        }



        public async Task<IEnumerable<Group>> GetByName(string name)
        {
            var data = await _context.Groups.Where(m => m.Name.Trim().Contains(name.Trim())).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Group>> SortByName(string message)
        {
            if (message == "asc")
            {
                var data = await _context.Groups.OrderBy(m => m.Name).ToListAsync();
                return data;
            }
            else
            {
                var data = await _context.Groups.OrderByDescending(m => m.Name).ToListAsync();

                return data;
            }
        }

        public async Task<IEnumerable<Group>> SortByCapacity(string message)
        {
            if (message == "asc")
            {
                var data = await _context.Groups.OrderBy(m => m.Capacity).ToListAsync();
                return data;
            }
            else
            {
                var data = await _context.Groups.OrderByDescending(m => m.Capacity).ToListAsync();

                return data;
            }
        }

        

      
        

        

        

       
        public async Task<Group> GetByGroupForStudentId(int id)
        {
            var data= await _context.GroupStudents.Include(m => m.Group).FirstOrDefaultAsync(m=>m.StudentId==id);
            var group = data.Group;
            return group;
        }

       
    }
}
