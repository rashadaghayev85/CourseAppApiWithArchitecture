using Domain.Entities;
using Service.DTOs.Educations;
using Service.DTOs.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IRoomService
    {
        Task CreateAsync(RoomCreateDto model);
        Task<IEnumerable<RoomDto>> GetAllWithAsync();

        Task<RoomDto> GetByIdAsync(int? id);
        Task DeleteAsync(int id);

        Task EditAsync(int id, RoomEditDto model);
        Task<IEnumerable<RoomDto>> GetByName(string name);
        Task<IEnumerable<RoomDto>> SortBySeatCount(string message);
        Task<IEnumerable<RoomDto>> SortByName(string message);
    }
}
