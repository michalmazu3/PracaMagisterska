using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Testing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {

                Task.Run(async ()=>await Call());
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

        private static async Task Call()
        {

            var httpClient = new HttpClient();
            for (int i = 0; i < 100; i++)
            {
                var response =await httpClient.GetAsync("https://teamleasing.azurewebsites.net/");
                var response2 = await httpClient.GetAsync("https://teamleasing.azurewebsites.net/search/Developers");
                var response3 = await httpClient.GetAsync("https://teamleasing.azurewebsites.net/search/Jobs");
                Console.WriteLine(i);
            }
        
        }
    }
}