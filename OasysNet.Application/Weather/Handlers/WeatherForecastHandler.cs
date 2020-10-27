using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using OasysNet.Application.Weather.Commands;

namespace OasysNet.Application.Weather.Handlers
{
    public class WeatherForecastHandler : IRequestHandler<WeatherForecastCommand, ValidationResult>
    {
        public WeatherForecastHandler()
        {
        }

        public Task<ValidationResult> Handle(WeatherForecastCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
