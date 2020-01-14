using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Threading;
using Wboy.Infrastructure.Core.Util.WebControl;

namespace Wboy.Domain
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        /// <summary>
        /// 获取当前 repository的unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The found entity or null.</returns>
        TEntity Find(params object[] keyValues);
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
        Task<TEntity> FindAsync(params object[] keyValues);
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(IEnumerable<TEntity> entities);

        ///// <summary>
        ///// Deletes the entity by the specified primary key.
        ///// </summary>
        ///// <param name="id">The primary key value.</param>
        //void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(IEnumerable<TEntity> entities);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Item to modify</param>
        void Modify(TEntity item);

        void Modify(TEntity item, List<string> fieldNames);

        /// <summary>
        /// 将current中的值更新到persisted，并设置persisted实体为已经修改.
        /// Sets modified entity into the repository. 
        /// When calling Commit() method in UnitOfWork 
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        void Merge(TEntity persisted, TEntity current);
        /// <summary>
        /// 获取所有的
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// <see cref="IRepository{TEntity}"/>
        /// </summary>
        /// <param name="id"><see cref="IRepository{TEntity}"/></param>
        /// <returns><see cref="IRepository{TEntity}"/></returns>
        TEntity Get(Guid id);
        /// <summary>
        /// <see cref="IRepository{TEntity}"/>
        /// </summary>
        /// <param name="id"><see cref="IRepository{TEntity}"/></param>
        /// <returns><see cref="IRepository{TEntity}"/></returns>
        Task<TEntity> GetAsync(Guid id);

        #region 新加的查询方法
        IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> condition, Pagination pagination);

        Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> condition, Pagination pagination);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindListAsync(IQueryable<TEntity> query, Pagination pagination);
       Task<IEnumerable<TEntity>> FindNoSortListAsync(IQueryable<TEntity> query, Pagination pagination);
        #endregion
    }
}
