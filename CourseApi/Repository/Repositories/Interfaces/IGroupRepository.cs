using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
       
        Task<IEnumerable<Group>> GetAllWithAsync();
        
        Task<List<Group>> GetByIdWithAsync(List<int>? id);
       
        Task<IEnumerable<Group>> GetByName(string name);
        Task<Group> GetByGroupForStudentId(int id);

        Task<IEnumerable<Group>> SortByName(string message);
        Task<IEnumerable<Group>> SortByCapacity(string message);
    }
}
