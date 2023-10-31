using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace BackendWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static readonly Random _random = new ();

        private static ConcurrentBag<BackendWebApi2Movie> _movies = new()
        {
            new BackendWebApi2Movie
            { 
                Id = Guid.NewGuid(), 
                Title = "Parasite", 
                Genre = "Thriller", 
                ReleaseDate = new DateTime(2019, 08, 11), 
                FromSource = "BackendWebApi2" },
            new BackendWebApi2Movie
            { 
                Id = Guid.NewGuid(), 
                Title = "The Big Lebowski", 
                Genre = "Comedy",
                ReleaseDate = new DateTime(1998, 05, 06), 
                FromSource = "BackendWebApi2" }
        };

        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BackendWebApi2Movie>>> Get()
        {            
            await Task.Delay(_random.Next(50, 2000));

            return _movies.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<BackendWebApi2Movie>> Post([FromBody] BackendWebApi2Movie movie) 
        {          
            await Task.Delay(_random.Next(500, 1500));

            movie.Id = Guid.NewGuid();
            movie.FromSource = "BackendWebApi2";

            _movies.Add(movie);

            return movie;
        }
    }
}
