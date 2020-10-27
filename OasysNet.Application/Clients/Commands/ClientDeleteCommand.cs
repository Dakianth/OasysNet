using System;
using OasysNet.Domain.Core.Messaging;

namespace OasysNet.Application.Clients.Commands
{
    public class ClientDeleteCommand : Command
    {
        public ClientDeleteCommand(Guid id)
        {
            Id = id;
        }
    }
}