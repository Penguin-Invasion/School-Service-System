using AutoMapper;
using SchoolServiceSystem.DTOs.Auth;
using SchoolServiceSystem.DTOs.Entry;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.DTOs.Student;
using SchoolServiceSystem.DTOs.User;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            /*CreateMap<CreateSchoolDTO, School>();
            CreateMap<UpdateSchoolDTO, School>();
            CreateMap<School, GetSchoolDTO>();

            CreateMap<LoginDTO, User>();
            CreateMap<User, GetUserWithTokenDTO>();
            CreateMap<User, GetUserDTO>();

            CreateMap<CreateServiceDTO, Service>();
            CreateMap<UpdateServiceDTO, Service>();
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            {
                return srcMember != null && !srcMember.ToString().Equals("0");
            }));

            CreateMap<Service, GetServiceDTO>();*/

            CreateMap<CreateSchoolDTO, School>();
            CreateMap<UpdateSchoolDTO, School>();
            CreateMap<School, GetSchoolDTO>();

            CreateMap<CreateServiceDTO, Service>();
            CreateMap<UpdateServiceDTO, Service>();
            CreateMap<Service, GetServiceDTO>();

            CreateMap<Entry, GetEntryDTO>().ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time.ToString("HH:mm:ss"))).ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Time.ToString("dd/MM/yyyy")));

            CreateMap<CreateStudentDTO, Student>();
            CreateMap<UpdateStudentDTO, Student>();
            CreateMap<Student, GetStudentDTO>();

            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, GetUserDTO>();
            CreateMap<User, GetUserWithTokenDTO>();
            CreateMap<LoginDTO, User>();

        }
    }
}
