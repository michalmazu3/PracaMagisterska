using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TeamLeasing.Models;
using Microsoft.EntityFrameworkCore;

namespace TeamLeasing.Infrastructure
{
    public class OptimizedUserManager: UserManager<User>
    {

        public OptimizedUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators,
            passwordValidators, keyNormalizer, errors, services, logger)
        {
            
        }

            public  Task<User> FindDeveloperUserByIdAsync(string userId) 
            {
                return Users.Include(c => c.DeveloperUser)
                            .ThenInclude(t=>t.Technology)
                            .FirstOrDefaultAsync(u => u.Id == userId);
            }
         
    }
}
