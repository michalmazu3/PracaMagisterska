using System.Linq;
using AutoMapper;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Developer.Account;
using TeamLeasing.ViewModels.Employee;
using TeamLeasing.ViewModels.Employee.Account;
using TeamLeasing.ViewModels.Job.SearchJob;

namespace TeamLeasing.Services.Mapping
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<RegistrationDeveloperViewModel, DeveloperUser>()
                .ForMember(f => f.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(f => f.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(f => f.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(f => f.City, opt => opt.MapFrom(src => src.City))
                .ForMember(f => f.Province, opt => opt.MapFrom(src => src.ChoosenProvince))
                .ForMember(f => f.Level, opt => opt.MapFrom(src => src.ChoosenLevel))
                .ForMember(src => src.IsFinishedUniversity, opt => opt.MapFrom(src => src.ChoosenIsFinishedUnivesity))
                .ForMember(f => f.University, opt => opt.MapFrom(src => src.University ?? "brak"))
                .ForMember(f => f.Experience, opt => opt.MapFrom(src => src.Experience))
                .ForMember(f => f.Cv, opt => opt.MapFrom(src => src.Cv))
                .ForMember(f => f.Photo, opt => opt.MapFrom(src => src.Photo))
                .ReverseMap();

            CreateMap<RegistrationDeveloperViewModel, User>()
                .ForMember(f => f.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(f => f.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(f => f.UserName, opt => opt.MapFrom(src => src.Username))
                .ReverseMap();

            CreateMap<RegistrationEmployeeViewModel, EmployeeUser>()
                .ForMember(f => f.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(f => f.Surname, opt => opt.MapFrom(src => src.Surname))
                  .ForMember(f => f.City, opt => opt.MapFrom(src => src.City))
                .ForMember(f => f.Province, opt => opt.MapFrom(src => src.ChoosenProvince))
                 .ForMember(f => f.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();

            CreateMap<RegistrationEmployeeViewModel, User>()
                .ForMember(f => f.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(f => f.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(f => f.UserName, opt => opt.MapFrom(src => src.Username))
                .ReverseMap();

            CreateMap<Job, JobViewModel>()
                .ForMember(p => p.Technology, opt => opt.MapFrom(src => src.Technology.Name))
                .ForMember(p => p.City, opt => opt.MapFrom(src => src.EmployeeUser.City))
                .ForMember(p => p.Province, opt => opt.MapFrom(src => src.EmployeeUser.Province))
                .ForMember(p => p.Company, opt => opt.MapFrom(src => src.EmployeeUser.Company)).ReverseMap();

            CreateMap<EditEmployeeAccountViewModel, EmployeeUser>()
                .ForMember(p=>p.Province, opt=>opt.MapFrom(src=>src.ChoosenProvince));
            CreateMap<EditEmployeeAccountViewModel, User>()
                .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

            CreateMap<Job, ApplicationViewModel>()
                .ForMember(p => p.Technology, opt => opt.MapFrom(src => src.Technology.Name))
                .ForMember(p => p.Company, opt => opt.MapFrom(src => src.EmployeeUser.Company));

        }
    }
}