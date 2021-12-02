using AutoMapper;
using SchoolServiceSystem.DTOs.School;
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
            CreateMap<CreateSchoolDTO, School>();
            CreateMap<UpdateSchoolDTO, School>();
            CreateMap<School, GetSchoolDTO>();

        }
    }
}
