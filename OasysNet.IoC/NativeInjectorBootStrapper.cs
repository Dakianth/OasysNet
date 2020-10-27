using System.Collections.Generic;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OasysNet.Application.Clients.Commands;
using OasysNet.Application.Clients.Handlers;
using OasysNet.Application.Clients.Queries;
using OasysNet.Application.Clients.Queries.Responses;
using OasysNet.Application.Weather.Handlers;
using OasysNet.Application.Weather.Queries;
using OasysNet.Data.Repositories;
using OasysNet.Domain.Interfaces.Data;
using OasysNet.Domain.Models;

namespace OasysNet.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton(new JsonSerializer
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            // Pipes

            // Domain - Events

            // Domain - Commands
            services.AddTransient<IRequestHandler<GetAllWeatherForecastQuery, IEnumerable<WeatherForecast>>, GetAllWeatherForecastQueryHandler>();

            services.AddTransient<IRequestHandler<ClientCreateCommand, ValidationResult>, ClientCreateCommandHandler>();
            services.AddTransient<IRequestHandler<ClientUpdateCommand, ValidationResult>, ClientUpdateCommandHandler>();
            services.AddTransient<IRequestHandler<ClientDeleteCommand, ValidationResult>, ClientDeleteCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllClientsQuery, IEnumerable<GetAllClientsResponse>>, GetAllClientsQueryHandler>();
            services.AddTransient<IRequestHandler<GetClientByIdQuery, GetClientByIdResponse>, GetClientByIdQueryHandler>();

            // Data
            services.AddTransient<IClientRepository, ClientRepository>();
        }
    }
}