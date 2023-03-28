using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace NakhleNakhoda.Services
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        #region Normal Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity? GetById(object id);

        /// <summary>
        /// Get all entity 
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        IEnumerable<TEntity?> GetAllByEnumerable();

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Save entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Save();

        #endregion

        #region Async Methods

        /// <summary>
        /// Get async entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity?> GetByIdAsync(object id);

        /// <summary>
        /// Get async all entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<IEnumerable<TEntity>> GetAllByEnumerableAsync();

        /// <summary>
        /// Get async all entity
        /// </summary>
        /// <returns>List<Entity></returns>
        Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAllByIncludeAsync(
            Expression<Func<TEntity, TEntity>> selector,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include
            );

        /// <summary>
        /// Insert async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Insert async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Update async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Save async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task SaveAsync();
        #endregion

        #region Properties
        IEnumerable<TEntity> GetAllByInclude(
            Expression<Func<TEntity, TEntity>> selector,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include
            );

        TEntity GetByInclude(Expression<Func<TEntity, TEntity>>? selector,
            Expression<Func<TEntity, bool>>? where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoNakhleNakhoda { get; }

        #endregion
    }
}
