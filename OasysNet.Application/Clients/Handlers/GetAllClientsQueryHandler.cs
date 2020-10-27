using System;
using System.Collections.Generic;
using System.Text;
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
    public class GetAllClientsQueryHandler : CommandHandler, IRequestHandler<GetAllClientsQuery, IEnumerable<GetAllClientsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public GetAllClientsQueryHandler(IMapper mapper, IClientRepository clientRepository)
            : base(clientRepository.UnitOfWork)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }
        public async Task<IEnumerable<GetAllClientsResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _clientRepository.GetAsync();
            return _mapper.Map<IEnumerable<GetAllClientsResponse>>(entities);
        }
    }
}
