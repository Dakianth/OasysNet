using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using OasysNet.Application.Clients.Commands;
using OasysNet.Domain.Core.Messaging;
using OasysNet.Domain.Interfaces.Data;

namespace OasysNet.Application.Clients.Handlers
{
    public class ClientDeleteCommandHandler : CommandHandler, IRequestHandler<ClientDeleteCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IClientRepository _clienteRepository;

        public ClientDeleteCommandHandler(IMediator mediator, IClientRepository clientRepository)
            : base(clientRepository.UnitOfWork)
        {
            _mediator = mediator;
            _clienteRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(ClientDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _clienteRepository.GetByIdAsync(request.Id);
            await _clienteRepository.DeleteAsync(entity);
            return await Commit();
        }
    }
}