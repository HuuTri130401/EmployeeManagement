using AutoMapper;
using Mona.EmployeeManagement.Domain.Models;
using Mona.EmployeeManagement.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Services.Commons
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            CreateMap<Employee, EmployeeRequest>().ReverseMap();
            CreateMap<Employee, EmployeeResponse>().ReverseMap();
            CreateMap<Employee, EmployeeRequestUpdate>().ReverseMap();
        }
    }
}
