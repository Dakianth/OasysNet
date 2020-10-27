using AutoMapper;
using OasysNet.Application.Clients.Commands;
using OasysNet.Application.Clients.Queries.Responses;
using OasysNet.Domain.Models;

namespace OasysNet.Application.Clients
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<ClientCreateCommand, Client>();
            CreateMap<ClientUpdateCommand, Client>();

            CreateMap<Client, GetAllClientsResponse>();
            CreateMap<Client, GetClientByIdResponse>();
        }
    }
}