using System;
using OasysNet.Application.Clients.Queries.Responses;
using OasysNet.Domain.Core.Messaging;

namespace OasysNet.Application.Clients.Queries
{
    public class GetClientByIdQuery : Command<GetClientByIdResponse>
    {
        public GetClientByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}