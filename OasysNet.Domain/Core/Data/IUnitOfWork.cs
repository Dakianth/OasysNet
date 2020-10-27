using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OasysNet.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        bool HasChanges();
    }
}
