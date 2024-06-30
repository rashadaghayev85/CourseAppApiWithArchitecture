using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student> GetByIdWithAsync(int? id);
        Task<IEnumerable<Student>> GetAllWithAsync();
        Task<IEnumerable<Student>> GetByNameOrSurname(string nameOrSurname);
        Task<IEnumerable<Student>> FilterBy(string ? name,string ? surname, int ? age );
    }
}
