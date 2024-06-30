using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.Groups;
using Service.DTOs.Room;
using Service.DTOs.Teachers;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public GroupService(IMapper mapper,
                           IGroupRepository groupRepo,
                           IStudentRepository studentRepo,
                           IRoomRepository roomRepo,
                           ILogger<StudentService> logger)

        {

            _mapper = mapper;
            _groupRepo = groupRepo;
            _studentRepo = studentRepo;
            _roomRepo = roomRepo;
            _logger = logger;
        }
        public async Task CreateAsync(GroupCreateDto model)
        {
            if (model == null) throw new ArgumentNullException();
            var room=await _roomRepo.GetById(model.RoomId);

            if (model.Capacity > room.SeatCount) throw new NotFoundException("This room is small");
         
            await _groupRepo.CreateAsync(_mapper.Map<Group>(model));
        }

        public async Task DeleteAsync(int id)
        {
            if (id==null)
            {
                _logger.LogWarning("Id is null");
                throw new ArgumentNullException();
            }
            var data = await _groupRepo.GetById((int)id);
            await _groupRepo.DeleteAsync(data);
        }

        public async Task EditAsync(int id, GroupEditDto model)
        {
            if (model == null) throw new ArgumentNullException();
            var data = await _groupRepo.GetById(id);

            if (data is null) throw new ArgumentNullException();

            var editData = _mapper.Map(model, data);
            await _groupRepo.EditAsync(editData);
        }

        public async Task<IEnumerable<GroupDto>> GetAllWithAsync()
        {
            _logger.LogInformation("Get All method is working");

            var data= await _groupRepo.GetAllWithAsync();
            return _mapper.Map<IEnumerable<GroupDto>>(data);
        }

        public async Task<GroupDto> GetByIdAsync(int ? id)
        {
            return _mapper.Map<GroupDto>(await _groupRepo.GetById((int)id));
        }
        public async Task<List<GroupDto>> GetById(List<int> ? id)
        {
            
            var existGroup = await _groupRepo.GetByIdWithAsync(id);
            var data= _mapper.Map<List<GroupDto>>(existGroup);
            return data;
        }

        public async Task<IEnumerable<GroupDto>> GetByName(string name)
        {
            if (name == null) throw new NotFoundException("Name is null");
            var data = _mapper.Map<IEnumerable<GroupDto>>(await _groupRepo.GetByName(name));

            return data;
        }

        public async Task<IEnumerable<GroupDto>> SortByName(string message)
        {
            if (message == null) throw new NotFoundException("Message is null");
            var data = _mapper.Map<IEnumerable<GroupDto>>(await _groupRepo.SortByName(message));

            return data;
        }

        public async Task<IEnumerable<GroupDto>> SortByCapacity(string message)
        {
            if (message == null) throw new NotFoundException("Message is null");
            var data = _mapper.Map<IEnumerable<GroupDto>>(await _groupRepo.SortByCapacity(message));

            return data;
        }

        public async Task<Group> GetByGroupForStudentId(int id)
        {
            return await _groupRepo.GetByGroupForStudentId(id);
        }
    }
}
