using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.DTOs.Educations;
using Service.DTOs.Students;
using Service.DTOs.Teachers;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IGroupRepository _groupRepo;
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherService> _logger;
        private readonly AppDbContext _context;

        public TeacherService(IMapper mapper,
                           IGroupRepository groupRepo,
                           ITeacherRepository teacherRepo,
                           ILogger<TeacherService> logger,
                           AppDbContext context)

        {

            _mapper = mapper;
            _groupRepo = groupRepo;
            _logger = logger;
            _teacherRepo = teacherRepo;
            _context=context;
        }

        public async Task CreateAsync(TeacherCreateDto model)
        {
            var existGroup = await _groupRepo.GetByIdWithAsync(model.GroupId);
            if (existGroup is null)
            {
                _logger.LogWarning($"Group is not found -{model.GroupId + "-" + DateTime.Now.ToString()}");
                throw new NotFoundException($"id-{model.GroupId} Group not found");
            }
            if (model == null) throw new ArgumentNullException();

            await _teacherRepo.CreateAsync(_mapper.Map<Teacher>(model));
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _teacherRepo.GetById(id);
            await _teacherRepo.DeleteAsync(data);
        }

        public async Task EditAsync(int id, TeacherEditDto model)
        {
            if (model == null) throw new ArgumentNullException();
            var data = await _teacherRepo.GetByIdWithAsync(id);

            if (data is null) throw new ArgumentNullException();

            _context.GroupTeachers.RemoveRange(data.GroupTeachers);
            foreach (var groupId in model.GroupId)
            {
                data.GroupTeachers.Add(new GroupTeacher { TeacherId = data.Id, GroupId = groupId });
            }

            var editData = _mapper.Map(model, data);
            await _teacherRepo.EditAsync(editData);
        }

        public async Task<IEnumerable<TeacherDto>> GetAllWithAsync()
        {
            _logger.LogInformation("Get All method is working");

            var teachers = await _teacherRepo.GetAllWithAsync();
            return _mapper.Map<List<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            return _mapper.Map<TeacherDto>(await _teacherRepo.GetByIdWithAsync(id));
        }

        public async Task<IEnumerable<TeacherDto>> GetByNameOrSurname(string nameOrSurname)
        {
            if (nameOrSurname == null) throw new NotFoundException("Name or surname is null");
            var data = _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.GetByNameOrSurname(nameOrSurname));

            return data;
        }

        public async Task<IEnumerable<TeacherDto>> SortByAge(string message)
        {
            if (message == null) throw new NotFoundException("Message is null");
            var data = _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.SortByAge(message));

            return data;
        }

        public async Task<IEnumerable<TeacherDto>> SortByName(string message)
        {
            if (message == null) throw new NotFoundException("Message is null");
            var data = _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.SortByName(message));

            return data;
        }

        public async Task<IEnumerable<TeacherDto>> SortBySalary(string message)
        {
            if (message == null) throw new NotFoundException("Message is null");
            var data = _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.SortBySalary(message));

            return data;
        }
    }
}
