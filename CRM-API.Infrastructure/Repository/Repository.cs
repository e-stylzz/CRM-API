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
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
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

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.CreatedDate = DateTime.UtcNow;
            //  var name = accessor.HttpContact.User.FindFirst(ClaimTypes.Name).Value;
            var name = "Eric Barb";
            entity.CreatedBy = name;
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null){
                throw new ArgumentException(nameof(entity));
            }
            entity.ModifiedDate = DateTime.UtcNow;
            //  var name = accessor.HttpContact.User.FindFirst(ClaimTypes.Name).Value;
            var name = "Eric Barb";
            entity.ModifiedBy = name;
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
