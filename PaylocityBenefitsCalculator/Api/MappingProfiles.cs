using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using System.Collections.Generic;

namespace Api
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<GetEmployeeDto, Employee>().ReverseMap();
            CreateMap<AddEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
            CreateMap<Employee, GetEmployeeDto>().ReverseMap();
            
            CreateMap<GetDependentDto, Dependent>().ReverseMap();
            CreateMap<AddDependentDto, Dependent>().ReverseMap();
            CreateMap<UpdateDependentDto, Dependent>().ReverseMap();
            CreateMap<Dependent,AddDependentWithEmployeeIdDto>().ReverseMap();   
        }
    }
}
