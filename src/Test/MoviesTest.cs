using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using TexoIt.Api;
using TexoIt.Api.ViewModels;
using TexoIt.Core.Entities;
using TexoIt.Infra.EntityFramework;

namespace TexoIt.Test;

[TestClass]
public class MoviesTest
{
    [TestMethod]
    public async Task Post()
    {
        var factory = new ApiTestFixture();
        var client = factory.CreateClient();
        var dbContext = (MovieContext?)factory.Services.GetService(typeof(MovieContext));
        dbContext.Database.EnsureCreated();

        using (var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
        {
            content.Add(new StreamContent(File.OpenRead("movielist.csv")), "file", "movielist.csv");
            using (var message = await client.PostAsync("/Movies", content))
            {
                var stringResponse = await message.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<OkResponse<bool>>(stringResponse);

                Assert.AreEqual(CustomHttpMessage.CODE_OK, model.Status.Code);
                Assert.AreEqual(true, model.Result);

                Assert.AreEqual(206, dbContext.Movies.Count());
            }
        }
    }

    [TestMethod]
    public async Task Get()
    {

        var factory = new ApiTestFixture();
        var client = factory.CreateClient();
        var dbContext = (MovieContext?)factory.Services.GetService(typeof(MovieContext));
        dbContext.Database.EnsureCreated();
        var studio = new Studio() { Name = "Studio" };
        var producer1 = new Producer() { Name = "Producer 1" };
        var producer2 = new Producer() { Name = "Producer 2" };
        var producer3 = new Producer() { Name = "Producer 3" };
        //seed
        CreateMovie(dbContext, studio, producer1, false, 1990, 1995, 2000);
        CreateMovie(dbContext, studio, producer1, true, 1980, 2010);
        CreateMovie(dbContext, studio, producer2, true, 1992, 1995, 2011);
        CreateMovie(dbContext, studio, producer2, false, 1996, 1999, 2013);
        CreateMovie(dbContext, studio, producer3, false, 1992, 1999, 2010);
        CreateMovie(dbContext, studio, producer3, true, 1990, 1993, 2020);
        dbContext.SaveChanges();

        var response = await client.GetAsync("Movies");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<WinnerResponse>(stringResponse);

        Assert.AreEqual(2, model.Min.Count());
        Assert.AreEqual(1, model.Max.Count());

        Assert.IsTrue(model.Min.Exists(p => p.Producer == "Producer 2"));
        Assert.AreEqual(3, model.Min.Where(p => p.Producer == "Producer 2").First().Interval);
        Assert.AreEqual(1992, model.Min.Where(p => p.Producer == "Producer 2").First().PreviousWin);
        Assert.AreEqual(1995, model.Min.Where(p => p.Producer == "Producer 2").First().FollowingWin);

        Assert.IsTrue(model.Min.Exists(p => p.Producer == "Producer 3"));
        Assert.AreEqual(3, model.Min.Where(p => p.Producer == "Producer 3").First().Interval);
        Assert.AreEqual(1990, model.Min.Where(p => p.Producer == "Producer 3").First().PreviousWin);
        Assert.AreEqual(1993, model.Min.Where(p => p.Producer == "Producer 3").First().FollowingWin);

        Assert.AreEqual("Producer 1", model.Max.First().Producer);
        Assert.AreEqual(30, model.Max.First().Interval);
        Assert.AreEqual(1980, model.Max.First().PreviousWin);
        Assert.AreEqual(2010, model.Max.First().FollowingWin);
    }

    private void CreateMovie(MovieContext dbContext, Studio studio, Producer producer, bool winner, params int[] years)
    {
        foreach (var year in years)
        {
            var movie = new Movie()
            {
                Year = year,
                Title = "Movie " + year,
                Winner = winner,
                Producers = new List<MovieProducer>()
                {
                    new MovieProducer() { Producer = producer }
                },
                Studios = new List<MovieStudio>()
                {
                    new MovieStudio() { Studio = studio}
                }
            };
            dbContext.Movies.Add(movie);
        }

    }
}