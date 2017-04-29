using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.ViewModels;

namespace TeamLeasing.Infrastructure.Helper
{
    public class LoadingDataToSidebarHelper :ILoadingDataToSidebarHelper
    {
        private readonly IConfigurationService _configurationService;
        public LoadingDataToSidebarHelper(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public List<NameValuePairSearchViewModel<T>> LoadColletion<T>(Func<IConfigurationService, IList<T>> func)
        {
            return func(_configurationService)
                .Select(w => new NameValuePairSearchViewModel<T>()
                {
                    Name = w,
                    Value = false
                }).ToList();
        }
        public async Task<List<NameValuePairSearchViewModel<T>>> LoadColletionAsync<T>(Func<IConfigurationService, Task<IList<T>>> func)
        {
            return await Task.Run(async () =>
            {
                return await func(_configurationService)
                    .ContinueWith(t =>
                    {
                        return t.Result.Select(w => new NameValuePairSearchViewModel<T>()
                        {
                            Name = w,
                            Value = false
                        }).ToList();
                    });

            });
        }
    }
}