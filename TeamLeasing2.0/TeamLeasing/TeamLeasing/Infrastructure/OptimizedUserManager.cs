using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService;
using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.Infrastructure;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.Services.UploadService;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Developer.Account;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Bcpg;
using TeamLeasing.ViewModels.Employee;
using TeamLeasing.ViewModels.Employee.Account;

namespace TeamLeasing.Infrastructure
{
    public class OptimizedDbManager : UserManager<User>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly IMapper _mapper;
        private readonly IConfigurationService _configurationService;


        public OptimizedDbManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
            IHttpContextAccessor contextAccessor, TeamLeasingContext teamLeasingContext, IMapper mapper, IConfigurationService configurationService
        )
            : base(store, optionsAccessor, passwordHasher, userValidators,
                passwordValidators, keyNormalizer, errors, services, logger)
        {
            _contextAccessor = contextAccessor;
            _teamLeasingContext = teamLeasingContext;
            _mapper = mapper;
            _configurationService = configurationService;
        }

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


            var result = await this.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                _teamLeasingContext.EmployeeUsers.Update(user.EmployeeUser);
                await _teamLeasingContext.SaveChangesAsync();
                user.PhoneNumber = !string.IsNullOrEmpty(model.Phone) ? model.Phone : user.PhoneNumber;
                return await this.UpdateAsync(user);

            }
            return result;
        }
        public async Task<User> CreateEmployeeUser(RegistrationEmployeeViewModel model)
        {
            User user = _mapper.Map<User>(model);

            var createUserResult = await this.CreateAsync(user, model.Password);
            if (createUserResult.Succeeded)
            {
                var id = await this.GetUserIdAsync(user);
                var resultAdd = await AddEmployeeuserToDb(model, id);
                if (resultAdd != true)
                {
                    throw new Exception(message: "Utworzenie użytkownka z przyczyn niewyjaśnionych nie powiodło się");
                }

                var resultAddRole = await AddRole(id, Roles.Employee);
                if (resultAddRole)
                {
                    return await this.FindByIdAsync(id);
                }
                else
                {
                    throw new Exception(
                        message: "Nadanie uprawnień użytkownikowi z przyczyn niewyjaśnionych nie powiodło się");

                }
            }
            else
            {
                throw new Exception(message: "Utworzenie użytkownka z przyczyn niewyjaśnionych nie powiodło się");
            }

        }

        #endregion

        #region Job

        public async Task<List<Job>> GetJobsForEmployee(string userId)
        {
            var employee = await this.FindEmployeeUserByIdAsync(userId);
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
            var developerUserJobDao = await this.GetDeveloperUsersJob(w => w.DeveloperUser.UserId == userId);
            return developerUserJobDao;

        }

        public async Task<bool> CreateJob(CreateJobViewModel model, User user)
        {
            Job job = _mapper.Map<Job>(model);
            job.IsHidden = false;
            job.StatusForEmployee = Enums.JobStatusForEmployee.InProgress;
            job.EmployeeUser = user.EmployeeUser;
            var technology = await this.GetTechnology(w => w.Name == model.ChoosenTechnology);
            job.Technology = technology.FirstOrDefault();

            await _teamLeasingContext.Jobs.AddAsync(job);
            var result = await _teamLeasingContext.SaveChangesAsync();

            return Convert.ToBoolean(result);
        }

        public async Task<int> FinishJob(int id)
        {
            var jobDao = await this.GetJob(w => w.Id == id);
            var job = jobDao.FirstOrDefault();

            job.IsHidden = true;
            job.StatusForEmployee = Enums.JobStatusForEmployee.Finished;
            foreach (var item in job.DeveloperUsers)
            {
                item.StatusForDeveloper = Enums.JobStatusForDeveloper.Finished;
            }

            _teamLeasingContext.Update(job);
            return await _teamLeasingContext.SaveChangesAsync();
        }

        public async Task<int> ResignJobApplication(string userId, int jobId)
        {
            var jobDao = await this.GetDeveloperUsersJob(w => w.DeveloperUser.UserId == userId && w.Job.Id == jobId);
            var jobToResign = jobDao.FirstOrDefault();
            jobToResign.StatusForDeveloper = Enums.JobStatusForDeveloper.Resignation;
            var r = _teamLeasingContext.DeveloperUserJob.Update(jobToResign);
            var result = await _teamLeasingContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> ApplyForJob(string userId, int jobId)
        {
            var developer = await this.GetDeveloperUser(w => w.UserId == userId)
                                      .ContinueWith(t => t.Result.First());
          
            if (!developer.Jobs.Any(a => a.JobId == jobId &&
                                        a.StatusForDeveloper == Enums.JobStatusForDeveloper.Applying))
                if (!developer.Jobs.Any(a => a.JobId == jobId))
                {
                    DeveloperUserJob developerUserJob = new DeveloperUserJob()
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

        #endregion

        #region Technology

        public async Task<List<Technology>> GetTechnology(Expression<Func<Technology, bool>> querry)
        {
            return await _teamLeasingContext.Technologies.Where(querry).ToListAsync();
        }

        #endregion

        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            IdentityResult result = new IdentityResult();
            var user = this.FindByIdAsync(userId);

            result = await this.ChangePasswordAsync(user.Result, currentPassword, newPassword);


            return result;
        }

        #region private

        private async Task<List<DeveloperUserJob>> GetDeveloperUsersJob(Expression<Func<DeveloperUserJob, bool>> querry)
        {
            return await _teamLeasingContext.DeveloperUserJob
                .Include(i => i.DeveloperUser)
                .Include(i => i.Job)
                .ThenInclude(i => i.EmployeeUser)
                .Where(querry ?? (w => true)).ToListAsync();
        }


        private async Task<bool> AddEmployeeuserToDb(RegistrationEmployeeViewModel model, string id)
        {
            EmployeeUser employeeUser = _mapper.Map<EmployeeUser>(model);

            employeeUser.UserId = id;
            var result = await _teamLeasingContext.EmployeeUsers.AddAsync(employeeUser)
                .ContinueWith(async t =>
                {
                    return await _teamLeasingContext.SaveChangesAsync();

                })
                .Result;

            return Convert.ToBoolean(result);
        }

        private async Task<bool> AddRole(string id, Roles role)
        {
            var addRoleResult = await this.AddToRoleAsync(await this.FindByIdAsync(id), role.ToString());
            return addRoleResult.Succeeded;
        }

        #endregion

    }
}
