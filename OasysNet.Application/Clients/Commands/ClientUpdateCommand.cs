using OasysNet.Domain.Core.Messaging;

namespace OasysNet.Application.Clients.Commands
{
    public class ClientUpdateCommand : Command
    {
        public string Name { get; set; }
    }
}