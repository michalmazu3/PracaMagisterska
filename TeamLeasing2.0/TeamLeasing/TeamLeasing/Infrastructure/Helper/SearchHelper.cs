using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamLeasing.Infrastructure.Helper
{
    public class SearchHelper : ISearchHelper
    {
        public Task<IEnumerable<T>> Intersection<T>(params IEnumerable<T>[] paramsList)
        {
            return Task.Run(() =>
            {
                paramsList = paramsList.Where(w => w.Count() != 0).ToArray();

                for (int i = 1; i < paramsList.Length; i++)
                {
                    paramsList[0] = paramsList[0].Intersect(paramsList[i]).ToList();
                }
                return paramsList[0];
            });

        }

        public List<T> Aplly<T>(Func<T, bool> querry, List<T> list)
        {
            return list.Where(querry).ToList();
        }
    }
}