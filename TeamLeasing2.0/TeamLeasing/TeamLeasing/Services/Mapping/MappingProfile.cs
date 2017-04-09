using System.Linq;
using AutoMapper;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;

namespace TeamLeasing.Services.Mapping
{
    public class MappingProfile : Profile
    {
        
            private readonly TeamLeasingContext _teamLeasingContext;

            public MappingProfile() : base()
            {
                // Add as many of these lines as you need to map your objects
                //CreateMap<User, UserDto>();
                //CreateMap<UserDto, User>();


                CreateMap<RegistrationDeveloperViewModel, DeveloperUser>()
                    .ForMember(f => f.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(f => f.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(f => f.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                    .ForMember(f => f.City, opt => opt.MapFrom(src => src.City))
                    .ForMember(f => f.Province, opt => opt.MapFrom(src => src.Province))
                    .ForMember(f => f.Level, opt => opt.MapFrom(src => src.ChoosenLevel))
                    .ForMember(src => src.IsFinishedUniversity,
                        opt => opt.MapFrom(src => src.ChoosenIsFinishedUnivesity))
                    .ForMember(f => f.University, opt => opt.MapFrom(src => src.University))
                    .ForMember(f => f.Experience, opt => opt.MapFrom(src => src.Experience))
                    .ForMember(f => f.Cv, opt => opt.MapFrom(src => src.Cv))
                    .ForMember(f => f.Photo, opt => opt.MapFrom(src => src.Photo))
                    .ReverseMap();

                CreateMap<RegistrationDeveloperViewModel, User>()
                    .ForMember(f => f.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(f => f.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                    .ForMember(f=>f.UserName, opt=>opt.MapFrom(src=>src.Email.Substring(0, src.Email.IndexOf("@"))))
                    .ReverseMap();
            }
        }
    
}