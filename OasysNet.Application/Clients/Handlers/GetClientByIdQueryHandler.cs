using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OasysNet.Application.Clients.Queries;
using OasysNet.Application.Clients.Queries.Responses;
using OasysNet.Domain.Core.Messaging;
using OasysNet.Domain.Interfaces.Data;

namespace OasysNet.Application.Clients.Handlers
{
    public class GetClientByIdQueryHandler : CommandHandler, IRequestHandler<GetClientByIdQuery, GetClientByIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public GetClientByIdQueryHandler(IMapper mapper, IClientRepository clientRepository)
            : base(clientRepository.UnitOfWork)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<GetClientByIdResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _clientRepository.GetByIdAsync(request.Id);
            return _mapper.Map<GetClientByIdResponse>(entity);
        }
    }
}