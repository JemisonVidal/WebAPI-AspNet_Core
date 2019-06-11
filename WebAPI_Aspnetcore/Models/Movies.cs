using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPI_Aspnetcore.Models
{

    public class MovieResponse
    {
        public List<MoviesByYear> MoviesByYear { get; set; }
        public int Total { get; set; }

    }

    public class MoviesByYear
    {

        [JsonProperty("movies")]
        public int Movies { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

    }

    public class Movies
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Datum
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string imdbID { get; set; }
    }

}
