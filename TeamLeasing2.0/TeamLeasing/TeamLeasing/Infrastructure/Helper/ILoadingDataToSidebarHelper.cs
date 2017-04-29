using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.ViewModels;

namespace TeamLeasing.Infrastructure.Helper
{
    public interface ILoadingDataToSidebarHelper
    {
        List<NameValuePairSearchViewModel<T>> LoadColletion<T>(Func<IConfigurationService, IList<T>> func);
        Task<List<NameValuePairSearchViewModel<T>>> LoadColletionAsync<T>(Func<IConfigurationService, Task<IList<T>>> func);

    }
}