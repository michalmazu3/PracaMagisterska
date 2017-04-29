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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TeamLeasing.Infrastructure
{
    public class OptimizedDbManager : UserManager<User>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly TeamLeasingContext _teamLeasingContext;

        public OptimizedDbManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
            IHttpContextAccessor contextAccessor, TeamLeasingContext teamLeasingContext)
            : base(store, optionsAccessor, passwordHasher, userValidators,
            passwordValidators, keyNormalizer, errors, services, logger)
        {
            _contextAccessor = contextAccessor;
            _teamLeasingContext = teamLeasingContext;
        }

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
                .Where(querry ?? (w => true))
                .ToListAsync();

        }

        public async Task<List<Job>> GetJob(Expression<Func<Job, bool>> querry = null)
        {
            return await _teamLeasingContext.Jobs
                 .Include(i => i.Technology)
                 .Include(j => j.EmployeeUser)
                 .Where(querry ?? (w => true))
                 .ToListAsync();
        }

    }
}
