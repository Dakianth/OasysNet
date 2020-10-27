using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OasysNet.Application.Weather.Queries;
using OasysNet.Domain.Models;

namespace OasysNet.Application.Weather.Handlers
{
    public class GetAllWeatherForecastQueryHandler : IRequestHandler<GetAllWeatherForecastQuery, IEnumerable<WeatherForecast>>
    {
        public GetAllWeatherForecastQueryHandler()
        {
        }

        private static readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<IEnumerable<WeatherForecast>> Handle(GetAllWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = _summaries[rng.Next(_summaries.Length)]
            })
            .ToArray();

            return new ValueTask<IEnumerable<WeatherForecast>>(result).AsTask();
        }
    }
}