using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace docker_core_couchbase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ILogger<WeatherForecastController> _logger;
        private IBucket _bucket;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBucketProvider bucketProvider)
        {
            _logger = logger;
            _bucket = bucketProvider.GetBucket("jul");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var id = Guid.NewGuid().ToString();

            _bucket.Insert(new Document<dynamic>
            {
                Id = id,
                Content = new
                {
                    hello = "microservice",
                    salut = "la julieta"
                }
            });


          
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
