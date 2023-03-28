using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NakhleNakhoda.Data.Context;
using System.Linq.Expressions;

namespace NakhleNakhoda.Services
{
    /// <summary>
    /// Represents the Entity Framework repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        private readonly IUnitOfWork _uow;

        private DbSet<TEntity>? _entities;

        #endregion

        #region Ctor

        protected Repository(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_uow is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            Save();
            return exception.ToString();
        }

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected async Task<string> GetFullErrorTextAndRollbackEntityChangesAsync(DbUpdateException exception)
        {
            //rollback entity changes
            if (_uow is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            await SaveAsync();
            return exception.ToString();
        }

        #endregion

        #region Normal Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity? GetById(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// Get all entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual IEnumerable<TEntity> GetAllByEnumerable()
        {
            return Entities.AsEnumerable();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Add(entity);
                Save();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.AddRange(entities);
                Save();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
                Save();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.UpdateRange(entities);
                Save();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Remove(entity);
                Save();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                Save();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Save entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public void Save()
        {
            _uow.SaveChanges();
        }

        #endregion



        #region Async Methods

        /// <summary>
        /// Get async entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Get async all entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllByEnumerableAsync()
        {
            return await Entities.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllByIncludeAsync(
            Expression<Func<TEntity, TEntity>> selector,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include
            )
        {
            IQueryable<TEntity> query = Entities;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }
            if (selector != null)
            {
                query = query.Select(selector);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Insert async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var s = await Entities.AddAsync(entity);
                await SaveAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
            }
        }

        /// <summary>
        /// Insert async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                await Entities.AddRangeAsync(entities);
                await SaveAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
            }
        }

        /// <summary>
        /// Update async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
                await SaveAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
            }
        }

        /// <summary>
        /// Update async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.UpdateRange(entities);
                await SaveAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
            }
        }

        /// <summary>
        /// Delete async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Remove(entity);
                await SaveAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
            }
        }

        /// <summary>
        /// Delete async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                await SaveAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
            }
        }

        /// <summary>
        /// Save async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public async Task SaveAsync()
        {
            await _uow.SaveChangesAsync();
        }

        #endregion

        #region Properties

        public virtual IEnumerable<TEntity> GetAllByInclude(
            Expression<Func<TEntity, TEntity>> selector,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include
            )
        {
            IQueryable<TEntity> query = Entities;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }
            if (selector != null)
            {
                query = query.Select(selector);
            }

            if (include != null)
            {
                query = include(query);
            }

            return query.ToList();
        }

        public virtual Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression) =>
             Entities.Where(expression).ToListAsync();

        public virtual TEntity GetByInclude(
            Expression<Func<TEntity, TEntity>>? selector,
            Expression<Func<TEntity, bool>>? where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include
            )
        {
            IQueryable<TEntity> query = Entities;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }
            if (selector != null)
            {
                query = query.Select(selector);
            }

            if (include != null)
            {
                query = include(query);
            }

            return query.First();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoNakhleNakhoda => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                _entities ??= _uow.Set<TEntity>();

                return _entities;
            }
        }

        #endregion
    }
}