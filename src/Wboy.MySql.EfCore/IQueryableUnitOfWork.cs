using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Wboy.Domain;

namespace Wboy.MySql.EfCore
{
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 返回一个IDbSet实例上下文中访问特定类型的实体
        /// the ObjectStateManager, and the underlying store. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        /// 对象设置为修改
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">设置为修改的实体</param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        void SetModified<TEntity>(TEntity entity, List<string> fieldNames) where TEntity : class;

        /// <summary>
        /// 在原始值应用当前值
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="original">原始值</param>
        /// <param name="current">当前值</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
    } 
}
