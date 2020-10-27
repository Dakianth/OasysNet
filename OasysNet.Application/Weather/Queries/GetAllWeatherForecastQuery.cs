using System.Collections.Generic;
using MediatR;
using OasysNet.Domain.Models;

namespace OasysNet.Application.Weather.Queries
{
    public class GetAllWeatherForecastQuery : IRequest<IEnumerable<WeatherForecast>>
    {
    }
}