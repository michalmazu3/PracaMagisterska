using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Testing
{
    internal class Program
    {
        private static DbContextOptions _contextOptions;

        private static void Main(string[] args)
        {
            try
            {
                _contextOptions = new DbContextOptionsBuilder()
                    .UseSqlServer(
                        "Server=tcp:teamleasingserver.database.windows.net,1433;Initial Catalog=TeamLeasingDb;Persist Security Info=False;User ID=MichalMazur;Password=Admin123$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                    .Options;
                var tasks = Enumerable.Range(0, 100).Select(p => TestDb());
                Task.WhenAll(tasks).Wait();
                while (ConsoleKey.Escape != Console.ReadKey().Key)
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task TestDb()
        {
            var context = new MyContext(_contextOptions);
            var result = await context.Technologies.ToListAsync();
            Console.WriteLine(result);
        }
        //    var httpClient = new HttpClient();
        //{


        //private static async Task Call()
        //    var response = await httpClient.GetAsync("https://teamleasing.azurewebsites.net/search/Developers");
        //}
    }
}