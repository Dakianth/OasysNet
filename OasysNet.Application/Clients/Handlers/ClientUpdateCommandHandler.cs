using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using OasysNet.Application.Clients.Commands;
using OasysNet.Domain.Core.Messaging;
using OasysNet.Domain.Interfaces.Data;

namespace OasysNet.Application.Clients.Handlers
{
    public class ClientUpdateCommandHandler : CommandHandler, IRequestHandler<ClientUpdateCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public ClientUpdateCommandHandler(IMediator mediator, IMapper mapper, IClientRepository clientRepository)
            : base(clientRepository.UnitOfWork)
        {
            _mediator = mediator;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _clientRepository.GetByIdAsync(request.Id);
            entity.Name = request.Name;
            return await Commit();
        }
    }
}