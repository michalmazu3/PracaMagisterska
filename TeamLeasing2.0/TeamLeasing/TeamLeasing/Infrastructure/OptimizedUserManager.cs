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
    public class OptimizedUserManager : UserManager<User>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public OptimizedUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger, IHttpContextAccessor contextAccessor)
            : base(store, optionsAccessor, passwordHasher, userValidators,
            passwordValidators, keyNormalizer, errors, services, logger)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<User> FindDeveloperUserByIdAsync(string userId)
        {
            return await Users.Include(c => c.DeveloperUser)
                        .ThenInclude(t => t.Technology)
                        .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<IEnumerable<User>> GetDeveloperUser(Expression<Func<User, bool>> querry = null)
        {
            return await Users.Include(c => c.DeveloperUser)
                .ThenInclude(t => t.Technology)
                .Where(querry ?? (w=>true)).ToListAsync();
           
        }

    }
}
