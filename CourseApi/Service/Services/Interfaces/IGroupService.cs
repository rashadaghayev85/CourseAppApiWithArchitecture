using Domain.Entities;
using Service.DTOs.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(GroupCreateDto model);
        Task<IEnumerable<GroupDto>> GetAllWithAsync();

        Task<GroupDto> GetByIdAsync(int? id);
        Task DeleteAsync(int id);

        Task EditAsync(int id, GroupEditDto model);
        Task<List<GroupDto>> GetById(List<int>? id);

        Task<IEnumerable<GroupDto>> GetByName(string name);

        Task<IEnumerable<GroupDto>> SortByName(string message);
        Task<IEnumerable<GroupDto>> SortByCapacity(string message);
        Task<Group> GetByGroupForStudentId(int id);
    }
}
