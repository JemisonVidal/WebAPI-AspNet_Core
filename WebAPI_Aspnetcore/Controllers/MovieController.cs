using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI_Aspnetcore.Models;
using Newtonsoft.Json;
using WebAPI_Aspnetcore.Models;
namespace WebAPI_Aspnetcore.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        [Route("api/movies/search")]
        public async Task<MovieResponse> Get(string title)
        {

            var baseAddress = new Uri($"https://jsonmock.hackerrank.com/api/movies/");
            //search /? Title ={ title}
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseAddress))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var objSerialize = JsonConvert.DeserializeObject<Movies>(content);

                    List<Movies> listMovies = new List<Movies>();

#if DEBUG 
                    {
                        objSerialize.total_pages = 2;

                    }

#endif
                    //BUSCANDO FILMES EM TODAS AS PAGINAS 
                    for (int i = 1; i <= objSerialize.total_pages; i++)
                    {
                        var movie = await Services.GetMoviesPage(title, i);
                        listMovies.Add(movie);
                    }

                    var listaMoviesByYearAux = new List<MoviesByYear>();
                    //PASSANDO PAGINA POR PAGINA 
                    foreach (var itens in listMovies)
                    {
                        //AGRUPAMENTO POR PAGINA
                        foreach (var item in itens.data.GroupBy(x => x.Year).ToList())
                        {
                            listaMoviesByYearAux.Add(new MoviesByYear { Year = item.Key, Movies = item.Count() });
                        }
                    };

                    //RETORNO
                    var responseMovies = new MovieResponse();
                    responseMovies.MoviesByYear = listaMoviesByYearAux.GroupBy(x => x.Year)
                              .Select(p => new MoviesByYear
                              {
                                  Year = p.Key,
                                  Movies = p.Sum(x => x.Movies)

                              }).ToList();

                    //responseMovies.MoviesByYear = listaAgrupadaTotal;
                    responseMovies.Total = responseMovies.MoviesByYear.Sum(x => x.Movies);
                    return responseMovies;

                }


            }
        }
    }
}
