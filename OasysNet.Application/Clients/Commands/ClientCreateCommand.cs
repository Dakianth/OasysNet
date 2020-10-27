using OasysNet.Domain.Core.Messaging;

namespace OasysNet.Application.Clients.Commands
{
    public class ClientCreateCommand : Command
    {
        public string Name { get; set; }
    }
}