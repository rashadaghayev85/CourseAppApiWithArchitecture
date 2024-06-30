using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<Teacher> GetByIdWithAsync(int? id);
        Task<IEnumerable<Teacher>> GetAllWithAsync();

        Task<IEnumerable<Teacher>> GetByNameOrSurname(string nameOrSurname);

        Task<IEnumerable<Teacher>> SortByName(string message);
        Task<IEnumerable<Teacher>> SortByAge(string message);
        Task<IEnumerable<Teacher>> SortBySalary(string message);
    }
}
