using System.Linq;
using AutoMapper;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Job.SearchJob;

namespace TeamLeasing.Services.Mapping
{
    public class MappingProfile : Profile
    {
        
 
            public MappingProfile( ) 
            {
                // Add as many of these lines as you need to map your objects
                //CreateMap<User, UserDto>();
                //CreateMap<UserDto, User>();

 
                CreateMap<RegistrationDeveloperViewModel, DeveloperUser>()
                    .ForMember(f => f.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(f => f.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(f => f.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                    .ForMember(f => f.City, opt => opt.MapFrom(src => src.City))
                    .ForMember(f => f.Province, opt => opt.MapFrom(src => src.ChoosenProvince))
                    .ForMember(f => f.Level, opt => opt.MapFrom(src => src.ChoosenLevel))
                    .ForMember(src => src.IsFinishedUniversity, opt => opt.MapFrom(src => src.ChoosenIsFinishedUnivesity))
                    .ForMember(f => f.University, opt => opt.MapFrom(src => src.University??"brak"))
                    .ForMember(f => f.Experience, opt => opt.MapFrom(src => src.Experience))
                    .ForMember(f => f.Cv, opt => opt.MapFrom(src => src.Cv))
                    .ForMember(f => f.Photo, opt => opt.MapFrom(src => src.Photo))
                    .ReverseMap();

                CreateMap<RegistrationDeveloperViewModel, User>()
                    .ForMember(f => f.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(f => f.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                    .ForMember(f=>f.UserName, opt=>opt.MapFrom(src=>src.Username))
                    .ReverseMap();



                CreateMap<Job, JobViewModel>()
                    .ForMember(p => p.Technology, opt => opt.MapFrom(src => src.Technology.Name))
                    .ForMember(p => p.City, opt => opt.MapFrom(src => src.EmployeeUser.City))
                    .ForMember(p => p.Province, opt => opt.MapFrom(src => src.EmployeeUser.Province))
                    .ForMember(p => p.Company, opt => opt.MapFrom(src => src.EmployeeUser.Company));

                

            }

        
        //developerUser.Technology = vm.ChoosenTechnology != null ? await _teamLeasingContext.Technologies
        //    .FirstOrDefaultAsync(f => f.Name == vm.ChoosenTechnology)
        //: developerUser.Technology;

        //developerUser.Experience = vm.Experience == null || vm.Experience< 0 ? developerUser.Experience : (int) vm.Experience;
        //developerUser.City = vm.City ?? developerUser.City;
        ////developerUser.Province = string.IsNullOrEmpty(vm.ChoosenProvince)
        ////    ? developerUser.Province
        ////    : (Enums.Province)Enum.Parse(typeof(Enums.Province), vm.ChoosenProvince);


        //developerUser.IsFinishedUniversity = string.IsNullOrEmpty(vm.ChoosenIsFinishedUnivesity)
        //    ? developerUser.IsFinishedUniversity
        //: (Enums.IsFinishedUniversity) Enum.Parse(typeof(Enums.IsFinishedUniversity), vm.ChoosenIsFinishedUnivesity);


        //developerUser.Level = string.IsNullOrEmpty(vm.ChoosenLevel)
        //    ? developerUser.Level
        //: (Enums.Level) Enum.Parse(typeof(Enums.Level), vm.ChoosenLevel);

        //developerUser.University = vm.University ?? developerUser.University;
        //developerUser.About = vm.About ?? developerUser.About;


        }
    
}