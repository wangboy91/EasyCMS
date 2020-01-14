using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wboy.Domain;
using Wboy.Infrastructure.Core;

namespace Wboy.MySql.EfCore
{
    /// <summary>
    ///  参考地址 https://github.com/arch/UnitOfWork
    /// </summary>
    public class EfUnitOfWork : DbContext, IQueryableUnitOfWork, ISql
    {
        //public EfUnitOfWork(DbContextOptions<DbContext> options)
        //    : base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(string.IsNullOrEmpty(SampleContext.ConnectionString))
                throw new Exception("Wboy.Infrastructure.Core.SampleContext.ConnectionString IsNullOrEmpty");
            optionsBuilder.UseMySql(SampleContext.ConnectionString);
            //optionsBuilder.UseMySql(@"Server=39.106.45.243;database=boxSystemDb;uid=wang;pwd=Wboy$2017;port=3306;SslMode=None");
            //optionsBuilder.UseMySql(SampleContext.Current.AppConfiguration.GetConnectionString("MySql"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory(100);
            base.OnModelCreating(modelBuilder);
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (var type in typesToRegister)
            {

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

        #region 公共方法
        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }
        /// <summary>
        /// 只更新变化字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = EntityState.Modified;
        }
        /// <summary>
        /// 通过反射设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldNames"></param>
        public void SetModified<TEntity>(TEntity entity, List<string> fieldNames) where TEntity : class
        {
            if (fieldNames != null && fieldNames.Count > 0)
            {
                base.Set<TEntity>().Attach(entity);
                foreach (var item in fieldNames)
                {
                    base.Entry<TEntity>(entity).Property(item).IsModified = true;
                }
            }
            else
            {
                base.Entry<TEntity>(entity).State = EntityState.Modified;
            }

        }
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            //if it is not attached, attach original and set current values
            Entry<TEntity>(original).CurrentValues.SetValues(current);
        }
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public int Commit(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
            {
                this.EnsureAutoHistory();
            }

            return base.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        public async Task<int> CommitAsync(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
            {
                this.EnsureAutoHistory();
            }

            return await base.SaveChangesAsync();
        }




        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }
        public int ExecuteCommand(string sqlCommand)
        {
            return Database.ExecuteSqlCommand(sqlCommand);
        }
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
        #endregion
    }
}
