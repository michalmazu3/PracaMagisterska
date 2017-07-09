using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Employee;
using TeamLeasing.ViewModels.Employee.Account;

namespace TeamLeasing.Infrastructure
{
    public class OptimizedDbManager : UserManager<User>
    {
        private readonly IConfigurationService _configurationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly TeamLeasingContext _teamLeasingContext;


        public OptimizedDbManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
            IHttpContextAccessor contextAccessor, TeamLeasingContext teamLeasingContext, IMapper mapper,
            IConfigurationService configurationService
        )
            : base(store, optionsAccessor, passwordHasher, userValidators,
                passwordValidators, keyNormalizer, errors, services, logger)
        {
            _contextAccessor = contextAccessor;
            _teamLeasingContext = teamLeasingContext;
            _mapper = mapper;
            _configurationService = configurationService;
        }

        public async Task<User> GetUser(string id)
        {
            return await _teamLeasingContext.Users.Include(i => i.DeveloperUser)
                .Include(i => i.EmployeeUser).FirstOrDefaultAsync(f => f.Id == id);
        }

        #region Technology

        public async Task<List<Technology>> GetTechnology(Expression<Func<Technology, bool>> querry)
        {
            return await _teamLeasingContext.Technologies.Where(querry).ToListAsync();
        }

        #endregion

        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var result = new IdentityResult();
            var user = FindByIdAsync(userId);

            result = await ChangePasswordAsync(user.Result, currentPassword, newPassword);


            return result;
        }


        #region Project

        public async Task<bool> CreateProject(CreateProjectViewModel model, User user)
        {
            var project = _mapper.Map<Project>(model);
            project.VacanciesRemain = project.NumberOfDeveloperNeeded;
            project.IsHidden = false;
            project.Status = Enums.JobStatusForEmployee.InProgress;
            project.EmployeeUser = user.EmployeeUser;
            await _teamLeasingContext.Project.AddAsync(project);
            var result = await _teamLeasingContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<List<Project>> GetProject(Expression<Func<Project, bool>> querry = null,
            bool isHidden = false)
        {
            return await _teamLeasingContext.Project
                .Include(i => i.DeveloperInProject)
                .ThenInclude(j => j.DeveloperUser)
                .ThenInclude(j => j.Technology)
                .Include(j => j.EmployeeUser)
                .Where(w => isHidden ? w.IsHidden == false || w.IsHidden : w.IsHidden == false)
                .Where(w => w.VacanciesRemain >= 1)
                .Where(querry ?? (w => true))
                .ToListAsync();
        }

        #endregion

        #region DeveloperUser

        public async Task<User> FindDeveloperUserByIdAsync(string userId)
        {
            return await Users.Include(c => c.DeveloperUser)
                .ThenInclude(t => t.Technology)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<DeveloperUser>> GetDeveloperUser(Expression<Func<DeveloperUser, bool>> querry = null)
        {
            return await _teamLeasingContext.DeveloperUsers
                .Include(t => t.Technology)
                .Include(f => f.Jobs)
                .Include(f => f.Jobs)
                .Where(querry ?? (w => true))
                .ToListAsync();
        }

        #endregion

        #region EmployeeUser

        public async Task<User> FindEmployeeUserByIdAsync(string userId)
        {
            return await Users.Include(c => c.EmployeeUser)
                .ThenInclude(t => t.Jobs)
                .ThenInclude(o => o.DeveloperUsers)
                .ThenInclude(h => h.DeveloperUser)
                .Include(r => r.EmployeeUser)
                .ThenInclude(h => h.Offers)
                .Include(t => t.EmployeeUser)
                .ThenInclude(g => g.Jobs)
                .ThenInclude(g => g.Technology)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }


        public async Task<IdentityResult> UpdateEmployeeUser(EditEmployeeAccountViewModel model, string userId)
        {
            var user = await FindEmployeeUserByIdAsync(userId);

            user.EmployeeUser.City = !string.IsNullOrEmpty(model.City) ? model.City : user.EmployeeUser.City;
            user.EmployeeUser.Province = !string.IsNullOrEmpty(model.ChoosenProvince)
                ? model.ChoosenProvince
                : user.EmployeeUser.Province;
            user.EmployeeUser.Company = !string.IsNullOrEmpty(model.Company)
                ? model.Company
                : user.EmployeeUser.Company;


            var result = await ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                _teamLeasingContext.EmployeeUsers.Update(user.EmployeeUser);
                await _teamLeasingContext.SaveChangesAsync();
                user.PhoneNumber = !string.IsNullOrEmpty(model.Phone) ? model.Phone : user.PhoneNumber;
                return await UpdateAsync(user);
            }
            return result;
        }

        public async Task<User> CreateEmployeeUser(RegistrationEmployeeViewModel model)
        {
            var user = _mapper.Map<User>(model);

            var createUserResult = await CreateAsync(user, model.Password);
            if (createUserResult.Succeeded)
            {
                var id = await GetUserIdAsync(user);
                var resultAdd = await AddEmployeeuserToDb(model, id);
                if (resultAdd != true)
                    throw new Exception("Utworzenie użytkownka z przyczyn niewyjaśnionych nie powiodło się");

                var resultAddRole = await AddRole(id, Roles.Employee);
                if (resultAddRole)
                    return await FindByIdAsync(id);
                throw new Exception(
                    "Nadanie uprawnień użytkownikowi z przyczyn niewyjaśnionych nie powiodło się");
            }
            throw new Exception("Utworzenie użytkownka z przyczyn niewyjaśnionych nie powiodło się");
        }

        #endregion

        #region Job

        public async Task<List<Job>> GetJobsForEmployee(string userId)
        {
            var employee = await FindEmployeeUserByIdAsync(userId);
            return employee.EmployeeUser.Jobs.ToList();
        }

        public async Task<List<Job>> GetJob(Expression<Func<Job, bool>> querry = null, bool isHidden = false)
        {
            return await _teamLeasingContext.Jobs
                .Include(i => i.Technology)
                .Include(j => j.EmployeeUser)
                .Include(i => i.DeveloperUsers)
                .ThenInclude(j => j.DeveloperUser)
                .Where(w => isHidden ? w.IsHidden == false || w.IsHidden : w.IsHidden == false)
                .Where(querry ?? (w => true))
                .ToListAsync();
        }

        public async Task<List<DeveloperUserJob>> GetJobsForDeveloperByUserId(string userId)
        {
            // var jobsDao = await this.GetJob(isHidden: true);
            //return jobsDao.Where(w => w.DeveloperUsers.Any(f => f.DeveloperUser.UserId == userId)).ToList();
            var developerUserJobDao = await GetDeveloperUsersJob(w => w.DeveloperUser.UserId == userId);
            return developerUserJobDao;
        }

        public async Task<bool> CreateJob(CreateJobViewModel model, User user)
        {
            var job = _mapper.Map<Job>(model);
            job.IsHidden = false;
            job.StatusForEmployee = Enums.JobStatusForEmployee.InProgress;
            job.EmployeeUser = user.EmployeeUser;
            var technology = await GetTechnology(w => w.Name == model.ChoosenTechnology);
            job.Technology = technology.FirstOrDefault();

            await _teamLeasingContext.Jobs.AddAsync(job);
            var result = await _teamLeasingContext.SaveChangesAsync();

            return Convert.ToBoolean(result);
        }

        public async Task<int> FinishJob(int id)
        {
            var jobDao = await GetJob(w => w.Id == id);
            var job = jobDao.FirstOrDefault();

            job.IsHidden = true;
            job.StatusForEmployee = Enums.JobStatusForEmployee.Finished;
            foreach (var item in job.DeveloperUsers)
                item.StatusForDeveloper = Enums.JobStatusForDeveloper.Finished;

            _teamLeasingContext.Update(job);
            return await _teamLeasingContext.SaveChangesAsync();
        }

        public async Task<int> ResignJobApplication(string userId, int jobId)
        {
            var jobDao = await GetDeveloperUsersJob(w => w.DeveloperUser.UserId == userId && w.Job.Id == jobId);
            var jobToResign = jobDao.FirstOrDefault();
            jobToResign.StatusForDeveloper = Enums.JobStatusForDeveloper.Resignation;
            var result = await _teamLeasingContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> ApplyForJob(string userId, int jobId)
        {
            var developer = await GetDeveloperUser(w => w.UserId == userId)
                .ContinueWith(t => t.Result.First());

            if (!developer.Jobs.Any(a => a.JobId == jobId &&
                                         a.StatusForDeveloper == Enums.JobStatusForDeveloper.Applying))
                if (!developer.Jobs.Any(a => a.JobId == jobId))
                {
                    var developerUserJob = new DeveloperUserJob
                    {
                        DeveloperUserId = developer.Id,
                        JobId = jobId,
                        StatusForDeveloper = Enums.JobStatusForDeveloper.Applying
                    };
                    await _teamLeasingContext.DeveloperUserJob.AddAsync(developerUserJob);
                    return await _teamLeasingContext.SaveChangesAsync();
                }
                else
                {
                    developer.Jobs.First(f => f.JobId == jobId).StatusForDeveloper =
                        Enums.JobStatusForDeveloper.Applying;
                    return await _teamLeasingContext.SaveChangesAsync();
                }

            return -1;
            // return await _teamLeasingContext.SaveChangesAsync();
        }

        public async Task<int> RejectJobApplication(int jobId, int developerId)
        {
            var developeUserJob = await GetDeveloperUsersJob(s => s.DeveloperUserId == developerId
                                                                  && s.JobId == jobId)
                .ContinueWith(t => t.Result.FirstOrDefault());
            developeUserJob.StatusForDeveloper = Enums.JobStatusForDeveloper.Rejected;
            var result = await _teamLeasingContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> AcceptJobApplication(int jobId, int developerId)
        {
            var developeUserJob = await GetDeveloperUsersJob(s => s.DeveloperUserId == developerId
                                                                  && s.JobId == jobId)
                .ContinueWith(t => t.Result.FirstOrDefault());
            developeUserJob.StatusForDeveloper = Enums.JobStatusForDeveloper.Accepted;
            developeUserJob.Job.IsHidden = true;
            developeUserJob.Job.StatusForEmployee = Enums.JobStatusForEmployee.Approve;

            var result = await _teamLeasingContext.SaveChangesAsync();
            return result;
        }

        #endregion

        #region private

        private async Task<List<DeveloperUserJob>> GetDeveloperUsersJob(Expression<Func<DeveloperUserJob, bool>> querry)
        {
            return await _teamLeasingContext.DeveloperUserJob
                .Include(i => i.DeveloperUser)
                .Include(i => i.Job)
                .ThenInclude(i => i.Technology)
                .Include(i => i.Job)
                .ThenInclude(i => i.EmployeeUser)
                .Where(querry ?? (w => true)).ToListAsync();
        }


        private async Task<bool> AddEmployeeuserToDb(RegistrationEmployeeViewModel model, string id)
        {
            var employeeUser = _mapper.Map<EmployeeUser>(model);

            employeeUser.UserId = id;
            var result = await _teamLeasingContext.EmployeeUsers.AddAsync(employeeUser)
                .ContinueWith(async t => await _teamLeasingContext.SaveChangesAsync())
                .Result;

            return Convert.ToBoolean(result);
        }

        private async Task<bool> AddRole(string id, Roles role)
        {
            var addRoleResult = await AddToRoleAsync(await FindByIdAsync(id), role.ToString());
            return addRoleResult.Succeeded;
        }

        #endregion

        #region Offer

        public async Task<bool> RejectOfferByDeveloper(int developerId, int offerId)
        {
            var offer = await GetOffer(w => w.DeveloperUserId == developerId && w.Id == offerId)
                .ContinueWith(t => t.Result.FirstOrDefault());
            offer.StatusForEmployee = Enums.OfferStatus.Rejected;
            offer.StatusForDeveloper = Enums.OfferStatus.Resignation;
            if (offer.Negotiation != null)
            {
                offer.Negotiation.StatusForDeveloper = Enums.NegotiationStatus.Resignation;
                offer.Negotiation.StatusForEmployee = Enums.NegotiationStatus.Rejected;
            }
            var result = await _teamLeasingContext.SaveChangesAsync();
            return result == 0 ? false : true;
        }

        public async Task<bool> CancelOfferByEmployee(int employeeId, int offerId)
        {
            var offer = await GetOffer(w => w.EmployeeUserId == employeeId && w.Id == offerId)
                .ContinueWith(t => t.Result.FirstOrDefault());
            offer.StatusForEmployee = Enums.OfferStatus.Canceled;
            offer.StatusForDeveloper = Enums.OfferStatus.Canceled;
            if (offer.Negotiation != null)
            {
                offer.Negotiation.StatusForDeveloper = Enums.NegotiationStatus.Canceled;
                offer.Negotiation.StatusForEmployee = Enums.NegotiationStatus.Canceled;
            }
            var result = await _teamLeasingContext.SaveChangesAsync();
            return result == 0 ? false : true;
        }


        public async Task<bool> AcceptOfferByDeveloperUser(int developerId, int offerId)
        {
            var result = await AcceptOffer(w => w.DeveloperUserId == developerId && w.Id == offerId);
            return result;
        }

        public async Task<bool> AcceptOfferByEmployeeUser(int employeeId, int offerId)
        {
            var result = await AcceptOffer(w => w.EmployeeUserId == employeeId && w.Id == offerId);
            return result;
        }

        private async Task<bool> AcceptOffer(Expression<Func<Offer, bool>> querry)
        {
            var offer = await GetOffer(querry)
                .ContinueWith(t => t.Result.FirstOrDefault());
            offer.StatusForEmployee = Enums.OfferStatus.Accepted;
            offer.StatusForDeveloper = Enums.OfferStatus.Accepted;
            if (offer.Negotiation != null)
            {
                offer.Negotiation.StatusForDeveloper = Enums.NegotiationStatus.Accepted;
                offer.Negotiation.StatusForEmployee = Enums.NegotiationStatus.Accepted;
            }
            var result = await _teamLeasingContext.SaveChangesAsync();
            return result == 0 ? false : true;
        }


        public async Task<List<Offer>> GetRecivedOfferByDeveloperUserId(int developerUserId)
        {
            var recivedOfferList = await GetOffer(w => w.DeveloperUserId == developerUserId);
            return recivedOfferList;
        }

        public async Task<List<Offer>> GetSentOfferByEmployeeUserId(int employeeUserId)
        {
            var sentOfferList = await GetOffer(s => s.EmployeeUserId == employeeUserId);
            return sentOfferList;
        }

        private async Task<List<Offer>> GetOffer(Expression<Func<Offer, bool>> querry)
        {
            var offerList = await _teamLeasingContext.Offers
                .Include(i => i.EmployeeUser)
                .Include(i => i.DeveloperUser)
                .Include(i => i.Technology)
                .Include(i => i.Negotiation)
                .Where(querry ?? (w => true))
                .ToListAsync();
            return offerList;
        }


        public async Task<bool> SendOffer(string userId, SendingOfferViewModel vm)
        {
            var user = await FindEmployeeUserByIdAsync(userId);
            var offer = _mapper.Map<Offer>(vm);
            offer.IsHidden = false;
            offer.StatusForDeveloper = Enums.OfferStatus.New;
            offer.StatusForEmployee = Enums.OfferStatus.InProgress;
            offer.Technology = await GetTechnology(s => s.Name == vm.ChoosenTechnology)
                .ContinueWith(t => t.Result.First());
            offer.EmployeeUserId = user.EmployeeUser.Id;
            var result = await AddOffer(offer);
            return result;
        }

        private async Task<bool> AddOffer(Offer offer)
        {
            var result = await _teamLeasingContext.Offers.AddAsync(offer)
                .ContinueWith(async t => await _teamLeasingContext.SaveChangesAsync());

            return Convert.ToBoolean(await result);
        }

        private async Task<Offer> FindOffer(int id)
        {
            return await _teamLeasingContext.Offers
                .Include(i => i.Negotiation)
                .Include(i => i.DeveloperUser)
                .Include(i => i.EmployeeUser)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        #endregion


        #region Negotiation

        public async Task<int> AddOrUpdateNegotiation(Negotiation negotiation, int offerId,
            Enums.NegotiationStatus developerStatus,
            Enums.NegotiationStatus employeeStatus)
        {
            negotiation.StatusForDeveloper = developerStatus;
            negotiation.StatusForEmployee = employeeStatus;
            var offer = await FindOffer(offerId);
            if (offer.Negotiation == null)
            {
                await _teamLeasingContext.Negotiation.AddAsync(negotiation);
            }
            else
            {
                _teamLeasingContext.Negotiation.Remove(offer.Negotiation);
                offer.Negotiation = negotiation;
                await _teamLeasingContext.Negotiation.AddAsync(offer.Negotiation);
            }
            offer.StatusForEmployee = Enums.OfferStatus.Negotiation;
            offer.StatusForDeveloper = Enums.OfferStatus.Negotiation;

            var returnResult = await _teamLeasingContext.SaveChangesAsync();
            return returnResult;
        }

        private async Task<Negotiation> FindNegotiation(int id)
        {
            if (_teamLeasingContext.Negotiation.Any(a => a.Id == id))
                return await _teamLeasingContext.Negotiation.FirstAsync(f => f.Id == id);
            return null;
        }

        #endregion
    }
}