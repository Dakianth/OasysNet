using FluentValidation.Results;
using MediatR;

namespace OasysNet.Application.Weather.Commands
{
    public class WeatherForecastCommand : IRequest<ValidationResult>
    {
    }
}