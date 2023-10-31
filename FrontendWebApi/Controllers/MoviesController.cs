using FrontendWebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontendWebApi.Controllers
{
    [ApiController]    
    [Route("[controller]")]
    //[Authorize]
    public class MoviesController : ControllerBase
    {       
        private static HttpClient _httpClient = new ();
        private readonly string _backendWWebApi2BaseUrl = "https://localhost:7157/api/movies";
        private readonly string _backendWWebApi3BaseUrl = "https://localhost:7241/api/movies";

        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var backendWebApi2Task = _httpClient.GetAsync(_backendWWebApi2BaseUrl, CancellationToken.None);
            var backendWebApi3Task = _httpClient.GetAsync(_backendWWebApi3BaseUrl, CancellationToken.None);

            await Task.WhenAll(backendWebApi2Task, backendWebApi3Task);

            List<Movie> allMovies = new();

            if (backendWebApi2Task.Result.IsSuccessStatusCode) 
            {               
                allMovies.AddRange(await backendWebApi2Task.Result.Content.ReadFromJsonAsync<List<Movie>>() ?? new List<Movie>());
            }

            if (backendWebApi3Task.Result.IsSuccessStatusCode)
            {
                allMovies.AddRange(await backendWebApi3Task.Result.Content.ReadFromJsonAsync<List<Movie>>() ?? new List<Movie>());
            }

            return allMovies;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Movie?>>> Post([FromBody] Movie movie)
        {
            var json = JsonConvert.SerializeObject(movie, Formatting.Indented);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var backendWebApi2Task = _httpClient.PostAsync(_backendWWebApi2BaseUrl, stringContent, CancellationToken.None);
            var backendWebApi3Task = _httpClient.PostAsync(_backendWWebApi3BaseUrl, stringContent, CancellationToken.None);

            await Task.WhenAll(backendWebApi2Task, backendWebApi3Task);

            List<Movie?> newMovies = new();

            if (backendWebApi2Task.Result.IsSuccessStatusCode)
            {
                newMovies.Add(await backendWebApi2Task.Result.Content.ReadFromJsonAsync<Movie?>());
            }

            if (backendWebApi3Task.Result.IsSuccessStatusCode)
            {
                newMovies.Add(await backendWebApi3Task.Result.Content.ReadFromJsonAsync<Movie?>());
            }

            return newMovies;
        }
    }
}