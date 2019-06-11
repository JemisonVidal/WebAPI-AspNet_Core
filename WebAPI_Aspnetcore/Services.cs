using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI_Aspnetcore.Models;

namespace WebAPI_Aspnetcore
{
    public class Services
    {
        public static async Task<Models.Movies> GetMoviesPage(string title, int numberPage)
        {
            var baseAdress = new Uri($"https://jsonmock.hackerrank.com/api/movies/search/?Title={title}&page={numberPage}");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseAdress))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Movies>(content);

                }
            }


        }
    }
}
