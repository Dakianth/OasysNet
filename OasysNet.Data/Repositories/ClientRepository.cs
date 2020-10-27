using OasysNet.Data.Contexts;
using OasysNet.Domain.Interfaces.Data;
using OasysNet.Domain.Models;

namespace OasysNet.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}