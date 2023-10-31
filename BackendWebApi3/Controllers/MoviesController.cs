using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace BackendWebApi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static readonly Random _random = new();

        private static ConcurrentBag<BackendWebApi3Movie> _movies = new()
        {
            new BackendWebApi3Movie 
            { 
                Id = Guid.NewGuid(), 
                Title = "Killers of the Flower Moon", 
                Genre = "Drama/Crime", 
                ReleaseDate = new DateTime(2023, 10, 15), 
                FromSource = "BackendWebApi3" }
        };

        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BackendWebApi3Movie>>> Get()
        {
            await Task.Delay(_random.Next(500, 2000));

            return _movies.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] BackendWebApi3Movie movie)
        {
            await Task.Delay(_random.Next(500, 1000));

            movie.Id = Guid.NewGuid();
            movie.FromSource = "BackendWebApi3";

            _movies.Add(movie);

            return movie;
        }
    }
}