using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamLeasing.Infrastructure.Helper
{
    public interface ISearchHelper
    {
        Task<IEnumerable<T>> Intersection<T>(params IEnumerable<T>[] paramsList);
        List<T> Aplly<T>(Func<T, bool> querry, List<T> list);
    }
}