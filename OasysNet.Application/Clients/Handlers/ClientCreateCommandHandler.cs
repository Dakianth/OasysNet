using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using OasysNet.Application.Clients.Commands;
using OasysNet.Domain.Core.Messaging;
using OasysNet.Domain.Interfaces.Data;
using OasysNet.Domain.Models;

namespace OasysNet.Application.Clients.Handlers
{
    public class ClientCreateCommandHandler : CommandHandler, IRequestHandler<ClientCreateCommand, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public ClientCreateCommandHandler(IMapper mapper, IClientRepository clientRepository)
            : base(clientRepository.UnitOfWork)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Client>(request);

            if (!entity.IsValid())
                return entity.ValidationResult;

            await _clientRepository.CreateAsync(entity);

            return await Commit();
        }
    }
}