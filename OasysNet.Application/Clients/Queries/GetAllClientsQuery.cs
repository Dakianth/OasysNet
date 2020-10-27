using System.Collections.Generic;
using OasysNet.Application.Clients.Queries.Responses;
using OasysNet.Domain.Core.Messaging;

namespace OasysNet.Application.Clients.Queries
{
    public class GetAllClientsQuery : Command<IEnumerable<GetAllClientsResponse>>
    {
    }
}