using Service.DTOs.Educations;
using Service.DTOs.Groups;
using Service.DTOs.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task CreateAsync(EducationCreateDto model);
        Task<IEnumerable<EducationDto>> GetAllWithAsync();

        Task<EducationDto> GetByIdAsync(int? id);
        Task DeleteAsync(int id);

        Task EditAsync(int id, EducationEditDto model);

        Task<IEnumerable<EducationDto>> GetByName(string name);
        
        Task<IEnumerable<EducationDto>> SortByName(string message);

    }
}
