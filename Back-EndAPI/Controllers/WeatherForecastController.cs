using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Back_EndAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public class WeatherForecast
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int TemperatureC { get; set; }
            public string? Summary { get; set; }
        }

        private static readonly List<WeatherForecast> Forecasts = new()
        {
            new WeatherForecast { Id = 1, Date = DateTime.Now.AddDays(1), TemperatureC = 25, Summary = "Warm" },
            new WeatherForecast { Id = 2, Date = DateTime.Now.AddDays(2), TemperatureC = 20, Summary = "Cool" },
            new WeatherForecast { Id = 3, Date = DateTime.Now.AddDays(3), TemperatureC = 15, Summary = "Chilly" }
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Forecasts;
        }

        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> Get(int id)
        {
            var item = Forecasts.FirstOrDefault(f => f.Id == id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Post(WeatherForecast forecast)
        {
            var nextId = Forecasts.Any() ? Forecasts.Max(f => f.Id) + 1 : 1;
            forecast.Id = nextId;
            Forecasts.Add(forecast);
            return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
        }
    }
}
