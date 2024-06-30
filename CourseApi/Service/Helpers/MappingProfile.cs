using AutoMapper;
using Domain.Entities;
using Service.DTOs.Educations;
using Service.DTOs.Groups;
using Service.DTOs.Room;
using Service.DTOs.Students;
using Service.DTOs.Teachers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ForMember(dest => dest.Groups, opt => opt.MapFrom(src =>
                src.GroupStudents.Select(gs => gs.Group)));


            //.ForMember(dest => dest.GroupsName, opt => opt.MapFrom(src =>
            //    src.GroupStudents.Select(gs => gs.Group.Name)));
            CreateMap<StudentCreateDto, Student>().ForMember(dest => dest.GroupStudents, opt => opt.MapFrom(src =>
        src.GroupId.Select(id => new GroupStudent { GroupId = id })));
            CreateMap<StudentEditDto, Student>().ForMember(dest => dest.GroupStudents, opt => opt.MapFrom(src =>
        src.GroupId.Select(id => new GroupStudent { GroupId = id })));


            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupEditDto, Group>();

            CreateMap<Education, EducationDto>();
            CreateMap<EducationCreateDto, Education>();
            CreateMap<EducationEditDto, Education>();


            CreateMap<Room, RoomDto>();
            CreateMap<RoomCreateDto, Room>();
            CreateMap<RoomEditDto, Room>();

            CreateMap<Teacher, TeacherDto>().ForMember(dest => dest.Groups, opt => opt.MapFrom(src =>
                src.GroupTeachers.Select(gs => gs.Group))); ;
            CreateMap<TeacherCreateDto, Teacher>().ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src =>
        src.GroupId.Select(id => new GroupTeacher { GroupId = id }))); ;
            CreateMap<TeacherEditDto, Teacher>().ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src =>
        src.GroupId.Select(id => new GroupTeacher { GroupId = id }))); ;
        }

    }
}