using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace IntegrationRabbitMQ.WebApi.Infra.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public virtual List<T> GetAll()
        {
            using DataContext context = new();
            return context.Set<T>().AsNoTracking().ToList();
        }

        public virtual T? Get(int id)
        {
            using DataContext context = new();
            IEntityType? model = context.Model.FindEntityType(typeof(T));
            IKey? key = model?.FindPrimaryKey();

            if (key != null)
            {
                IProperty property = key.Properties[0];
                string columnName = property.Name;

                ParameterExpression parameter = Expression.Parameter(typeof(T), "entity");
                MemberExpression propertyExpression = Expression.Property(parameter, columnName);
                BinaryExpression equalsExpression = Expression.Equal(propertyExpression, Expression.Constant(id));
                Expression<Func<T, bool>> lambdaExpression = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);

                return context.Set<T>().FirstOrDefault(lambdaExpression);
            }

            return default;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            using DataContext context = new();
            var model = context.Model.FindEntityType(typeof(T));
            var key = model.FindPrimaryKey();
            if (key != null)
            {
                var property = key.Properties[0];
                var columnName = property.Name;

                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyExpression = Expression.Property(parameter, columnName);
                var equalsExpression = Expression.Equal(propertyExpression, Expression.Constant(id));
                var lambdaExpression = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);

                return await context.Set<T>().FirstOrDefaultAsync(lambdaExpression);
            }

            return null;
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            DataContext context = new();
            return context.Set<T>().Where(predicate).AsQueryable();
        }

        public virtual void Insert(T entity)
        {
            using DataContext context = new();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public virtual async Task InsertAsync(T entity)
        {
            using DataContext context = new();
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual void InsertRange(List<T> entities)
        {
            using DataContext context = new();
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }

        public virtual async Task InsertRangeAsync(List<T> entities)
        {
            using DataContext context = new();
            await context.Set<T>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            using DataContext context = new();
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            using DataContext context = new();
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public virtual void DeleteRange(List<T> entities)
        {
            using DataContext context = new();
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
        }
    }
}