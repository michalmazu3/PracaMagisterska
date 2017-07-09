using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Developer.Account;
using TeamLeasing.ViewModels.Employee;
using TeamLeasing.ViewModels.Employee.Account;
using TeamLeasing.ViewModels.Job.SearchJob;
using TeamLeasing.ViewModels.Project.SearchProject;

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
                .ForMember(p => p.Province, opt => opt.MapFrom(src => src.ChoosenProvince));
            CreateMap<EditEmployeeAccountViewModel, User>()
                .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

            CreateMap<DeveloperUserJob, ApplicationViewModel>()
                .ForMember(p => p.Technology, opt => opt.MapFrom(src => src.Job.Technology.Name))
                .ForMember(p => p.Company, opt => opt.MapFrom(src => src.Job.EmployeeUser.Company))
                .ForMember(p => p.Price, opt => opt.MapFrom(src => src.Job.Price))
                .ForMember(p => p.Title, opt => opt.MapFrom(src => src.Job.Title))
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.JobId))
                .ForMember(p => p.StatusForDeveloper, opt => opt.MapFrom(src => src.StatusForDeveloper));


            //.ForMember(p=>p.StatusForDeveloper, opt=>opt.MapFrom(src=>src.DeveloperUsers.Where(w=>w.DeveloperUserId==src. )));

            CreateMap<DeveloperUserJob, ApplyingDeveloper>()
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.DeveloperUser.Name))
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.DeveloperUser.Id))
                .ForMember(p => p.Surname, opt => opt.MapFrom(src => src.DeveloperUser.Surname));

            CreateMap<Job, JobWithApplicationsviewModel>()
                .ForMember(p => p.JobId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Status, opt => opt.MapFrom(src => src.StatusForEmployee))
                .ForMember(p => p.Technology, opt => opt.MapFrom(src => src.Technology.Name))
                .ForMember(p => p.ApplyingDevelopers,
                    opt => opt.MapFrom(source => Convert(source.DeveloperUsers.ToList())));


            CreateMap<CreateJobViewModel, Job>()
                .ForMember(p => p.EmploymentType, opt => opt.MapFrom(src => src.ChoosenEmploymentType ?? ""))
                .ForMember(p => p.Descritpion, opt => opt.MapFrom(src => src.Descritpion.Between("<body>", "</body>")));

            //CreateMap<SendingOfferViewModel, Offer>()
            //    .ForMember(p => p.Level,
            //        opt => opt.MapFrom(
            //            src => (Enums.Level) Enum.Parse(typeof(Enums.Level),
            //                src.ChoosenLevel)))
            //    .ForMember(p => p.EmploymentType, opt => opt.MapFrom(src => (Enums.EmploymentType) Enum.Parse(
            //        typeof(Enums.EmploymentType),
            //        src.ChoosenEmploymentType)));

            CreateMap<Offer, RecivedOfferViewModel>()
                .ForMember(p => p.OfferId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Company, opt => opt.MapFrom(src => src.EmployeeUser.Company))
                .ForMember(p => p.EmploymentType, opt => opt.MapFrom(src => src.EmploymentType.GetAttribute().Name))
                .ForMember(p => p.Level, opt => opt.MapFrom(src => src.Level.GetAttribute().Name))
                .ForMember(p => p.Technology, opt => opt.MapFrom(src => src.Technology.Name))
                .ForMember(p => p.NegotiationViewModel, opt => opt.MapFrom(src => src.Negotiation))
                .ForMember(p => p.StatusForDeveloper,
                    opt => opt.MapFrom(src => src.StatusForDeveloper.GetAttribute().Name));

            CreateMap<SendingOfferViewModel, Offer>()
                .ForMember(p => p.AdditionalInformation, opt => opt.MapFrom(src => src.AdditionalInformation))
                .ForMember(p => p.EmploymentType, opt => opt
                    .MapFrom(src => EnumExtansion.Values<Enums.EmploymentType>()
                        .FirstOrDefault(w => w.GetAttribute().Name == src.ChoosenEmploymentType)))
                .ForMember(p => p.Level, opt => opt
                    .MapFrom(src => EnumExtansion.Values<Enums.Level>()
                        .FirstOrDefault(w => w.GetAttribute().Name == src.ChoosenLevel)))
                .ForMember(p => p.ConstSalary, opt => opt.MapFrom(src => src.ConstSalary))
                .ForMember(p => p.MinSalary, opt => opt.MapFrom(src => src.MinSalary))
                .ForMember(p => p.MaxSalary, opt => opt.MapFrom(src => src.MaxSalary));


            CreateMap<Offer, SentOfferViewModel>()
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.DeveloperUser.Name))
                .ForMember(p => p.Surname, opt => opt.MapFrom(src => src.DeveloperUser.Surname))
                .ForMember(p => p.DeveloperUserId, opt => opt.MapFrom(src => src.DeveloperUser.Id))
                .ForMember(p => p.EmploymentType, opt => opt.MapFrom(src => src.EmploymentType.GetAttribute().Name))
                .ForMember(p => p.Level, opt => opt.MapFrom(src => src.Level.GetAttribute().Name))
                .ForMember(p => p.Technology, opt => opt.MapFrom(src => src.Technology.Name))
                .ForMember(p => p.StatusForEmployee,
                    opt => opt.MapFrom(src => src.StatusForEmployee.GetAttribute().Name))
                .ForMember(p => p.OfferId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.NegotiationViewModel, opt => opt.MapFrom(src => src.Negotiation));

            CreateMap<Negotiation, NegotiationViewModel>()
                .ForMember(p => p.EmploymentType, opt => opt.MapFrom(src => src.EmploymentType.GetAttribute().Name))
                .ForMember(p => p.StatusForEmployee,
                    opt => opt.MapFrom(src => src.StatusForEmployee.GetAttribute().Name))
                .ForMember(p => p.StatusForDeveloper,
                    opt => opt.MapFrom(src => src.StatusForDeveloper.GetAttribute().Name));

            CreateMap<NegotiationViewModel, Negotiation>()
                .ForMember(p => p.EmploymentType,
                    opt => opt.MapFrom(
                        src => EnumExtansion.ValueByDisplayName<Enums.EmploymentType>(src.EmploymentType)));

            CreateMap<Negotiation, Negotiation>().ReverseMap();
            CreateMap<CreateProjectViewModel, Project>()
                .ForMember(p => p.Descritpion, opt => opt.MapFrom(src => src.Descritpion.Between("<body>", "</body>")))
                .ForMember(p => p.ProjectType, opt => opt.MapFrom(src => src.ChoosenProjectType));


            CreateMap<Project, ProjectViewModel>()
                .ForMember(p => p.City, opt => opt.MapFrom(src => src.EmployeeUser.City))
                .ForMember(p => p.Province, opt => opt.MapFrom(src => src.EmployeeUser.Province))
                .ForMember(p => p.Company, opt => opt.MapFrom(src => src.EmployeeUser.Company)).ReverseMap();
        }

        private List<ApplyingDeveloper> Convert(List<DeveloperUserJob> source)
        {
            var list = new List<ApplyingDeveloper>();
            foreach (var item in source)
                list.Add(new ApplyingDeveloper
                {
                    Name = item.DeveloperUser.Name,
                    Surname = item.DeveloperUser.Surname,
                    Id = item.DeveloperUser.Id,
                    Status = item.StatusForDeveloper
                });
            return list;
        }
    }
}