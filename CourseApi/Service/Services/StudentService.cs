using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.DTOs.Groups;
using Service.DTOs.Students;
using Service.DTOs.Teachers;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGroupRepository _groupRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;
        

        public StudentService(IMapper mapper,
                           IGroupRepository groupRepo,
                           IStudentRepository studentRepo,
                           ILogger<StudentService> logger)

        {

            _mapper = mapper;
            _groupRepo = groupRepo;
            _studentRepo = studentRepo;
            _logger = logger;
           
        }
        public async Task CreateAsync(StudentCreateDto model)
        {
            var existGroup = await _groupRepo.GetByIdWithAsync(model.GroupId);
            if (existGroup is null)
            {
                _logger.LogWarning($"Group is not found -{model.GroupId + "-" + DateTime.Now.ToString()}");
                throw new NotFoundException($"id-{model.GroupId} Group not found");
            }
            if (model == null) throw new ArgumentNullException();
          
            foreach (var item in existGroup)
            {
                if(item.Capacity > item.StudentCount)
                {
                    item.StudentCount++;
                    await _groupRepo.EditAsync(item);
            await _studentRepo.CreateAsync(_mapper.Map<Student>(model));
                }
                else
                {
                    throw new NotFoundException($"Group is full");
                }
            }

        }

        public async Task DeleteAsync(int id)
        {

            var data = await _studentRepo.GetById(id);
            
           var group= await _groupRepo.GetByGroupForStudentId(id);
            group.StudentCount--;
            await _groupRepo.EditAsync(group);
            await _studentRepo.DeleteAsync(data);
           

        }

        public async Task EditAsync(int id, StudentEditDto model)
        {
            if (model == null) throw new ArgumentNullException();
            var data = await _studentRepo.GetByIdWithAsync(id);

            if (data is null) throw new ArgumentNullException();

            var editData = _mapper.Map(model, data);
            await _studentRepo.EditAsync(editData);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            return _mapper.Map<StudentDto>(await _studentRepo.GetByIdWithAsync(id));
        }
        public async Task<IEnumerable<StudentDto>> GetAllWithCountryAsync()
        {
            return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepo.GetAllWithAsync());
        }

        public async Task<IEnumerable<StudentDto>> GetAllWithAsync()
        {
            _logger.LogInformation("Get All method is working");

            var students= await _studentRepo.GetAllWithAsync();
            return _mapper.Map<List<StudentDto>>(students);
           
        }

        public async Task<IEnumerable<StudentDto>> GetByNameOrSurname(string nameOrSurname)
        {
            if (nameOrSurname == null) throw new NotFoundException("Name or surname is null");
            var data = _mapper.Map<IEnumerable<StudentDto>>(await _studentRepo.GetByNameOrSurname(nameOrSurname));

            return data;
        }

        public async Task<IEnumerable<StudentDto>> FilterBy(string? name, string? surname, int? age)
        {
            var data = _mapper.Map<IEnumerable<StudentDto>>(await _studentRepo.FilterBy(name,surname,age));

            return data;
        }
    }
}
