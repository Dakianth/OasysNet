using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OasysNet.Data.Mappings;
using OasysNet.Domain.Core.Data;

namespace OasysNet.Data.Contexts
{
    public class ApplicationContext: DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add Maps
            modelBuilder.ApplyConfiguration(new ClientMap());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync()
        {
            var success = await SaveChangesAsync() > 0;
            return success;
        }

        public bool HasChanges()
        {
            var hasChanges = ChangeTracker.HasChanges();
            return hasChanges;
        }
    }
}
