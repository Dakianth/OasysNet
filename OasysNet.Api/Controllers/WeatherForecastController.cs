using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OasysNet.Application.Weather.Queries;
using OasysNet.Domain.Models;

namespace OasysNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            var result = await _mediator.Send(new GetAllWeatherForecastQuery());
            return Ok(result);
        }
    }
}