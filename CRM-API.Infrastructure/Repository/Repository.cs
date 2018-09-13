using System;
using System.Collections.Generic;
using System.Linq;
using CRMAPI.Domain;
using CRMAPI.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CRMAPI.Infrastructure.Repository
{

    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
    }

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CRMProviderDbContext context;
        private DbSet<T> entities;

        public Repository(CRMProviderDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

    }
}
