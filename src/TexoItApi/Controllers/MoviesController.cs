using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using TexoIt.Api.ViewModels;
using TexoIt.Core.Entities;
using TexoIt.Core.Interfaces;
using TexoIt.Core.Specifications;

namespace TexoIt.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<MoviesController> _logger;
    private readonly IAsyncRepository<Movie> repMovie;
    private readonly IAsyncRepository<Producer> repProducer;
    private readonly IAsyncRepository<Studio> repStudio;

    public MoviesController(ILogger<MoviesController> logger,
        IAsyncRepository<Movie> repMovie,
        IAsyncRepository<Producer> repProducer,
        IAsyncRepository<Studio> repStudio)
    {
        _logger = logger;
        this.repMovie = repMovie;
        this.repProducer = repProducer;
        this.repStudio = repStudio;
    }

    [HttpPost]
    public async Task<ActionResult<OkResponse<bool>>> Post(IFormFile file)
    {
        try
        {
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                using (var csv = new CsvReader(reader, GetCsvConf()))
                {

                    csv.Context.RegisterClassMap<MovieRequestConf>();
                    var movies = csv.GetRecords<MovieRequest>().ToList();
                    await WriteMovies(movies);
                }
            }
            return new OkResponse<bool>(true);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ServerErrorResponse(CustomHttpMessage.CODE_INTERNAL_ERROR, ex.Message));
        }
    }

    [HttpGet()]
    public async Task<ActionResult<WinnerResponse>> Get()
    {
        try
        {
            var intervals = new List<MovieInterval>();
            var movies = await repMovie.ListAsync(new MovieWinnerSpecification());
            var tmp = from m in movies
                      from p in m.Producers
                      select new { p.Producer.Name, Movie = m };
            foreach (var item in tmp.OrderBy(p => p.Name).ThenBy(p => p.Movie.Year))
            {
                var previus = tmp.Where(p => p.Name == item.Name && p.Movie.Title != item.Movie.Title && p.Movie.Year <= item.Movie.Year)
                    .OrderByDescending(p => p.Movie.Year).FirstOrDefault();
                if (previus != null)
                    intervals.Add(new MovieInterval()
                    {
                        Producer = item.Name,
                        Interval = item.Movie.Year - previus.Movie.Year,
                        PreviousWin = previus.Movie.Year,
                        FollowingWin = item.Movie.Year
                    });
            }

            return new WinnerResponse()
            {
                Min = intervals.Where(p => p.Interval == intervals.Min(m => m.Interval)).ToList(),
                Max = intervals.Where(p => p.Interval == intervals.Max(m => m.Interval)).ToList(),
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ServerErrorResponse(CustomHttpMessage.CODE_INTERNAL_ERROR, ex.Message));
        }
    }

    private MovieResponse ConvertToResponse(Movie movie)
    {
        return new MovieResponse()
        {
            Year = movie.Year,
            Title = movie.Title,
            Winner = movie.Winner,
            Producers = movie.Producers.Select(p => p.Producer.Name).ToList(),
            Studios = movie.Studios.Select(p => p.Studio.Name).ToList(),
        };
    }

    private async Task WriteMovies(List<MovieRequest> movies)
    {
        foreach (var movie in movies)
        {
            var entity = (await repMovie.ListAsync(new MovieByYearAndTitleSpecification(movie.Year, movie.Title))).FirstOrDefault();
            if (entity == null)
            {
                entity = new Movie();
                entity.Year = movie.Year;
                entity.Title = movie.Title;
                entity.Winner = movie.Winner;
                foreach (var studio in movie.Studios)
                {
                    var entityStudio = (await repStudio
                        .ListAsync(new StudioByNameSpecification(studio)))
                        .FirstOrDefault() ?? new Studio() { Name = studio };
                    entity.Studios.Add(new MovieStudio() { Studio = entityStudio });
                }
                foreach (var producer in movie.Producers)
                {
                    var entityProcuder = (await repProducer
                        .ListAsync(new ProducerByNameSpecification(producer)))
                        .FirstOrDefault() ?? new Producer() { Name = producer };
                    entity.Producers.Add(new MovieProducer() { Producer = entityProcuder });
                }
                await repMovie.AddAsync(entity);
            }
        }
    }

    private CsvConfiguration GetCsvConf()
    {
        return new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding
            Delimiter = ";" // The delimiter is a comma
        };

    }
}
