using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wboy.Domain.PlatformModule.Entities;
using Wboy.Domain.PlatformModule.IRepository;
using Wboy.Infrastructure.Core.Util.WebControl;

namespace Wboy.MySql.EfCore.PlatformModule.Repository
{
    /// <summary>
    /// 文件 仓储类
    /// </summary>
    public class OssFileRepository : Repository<OssFileEntity>, IOssFileRepository
    {
        #region 基础属性
        public OssFileRepository(EfUnitOfWork unitOfWork)
           : base(unitOfWork) { }

        private EfUnitOfWork CurrentUnitOfWork => UnitOfWork as EfUnitOfWork;
        private DbSet<OssFolderEntity> _ossFolderDbSet;
        public DbSet<OssFolderEntity> OssFolderDbSet => _ossFolderDbSet ?? (_ossFolderDbSet = CurrentUnitOfWork.Set<OssFolderEntity>());
        #endregion

        #region 获取数据
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<OssFileEntity>> GetPageList(Pagination pagination, string keyword)
        {
            var query = _dbSet.Where(x => true);
            return FindListAsync(query, pagination);
        }
        /// <summary>
        /// 可用图片分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        public Task<IEnumerable<OssFileEntity>> GetEnablePicturePageList(Pagination pagination, Guid? folderId)
        {
            var query = _dbSet.Where(x => x.FileType == Domain.ValueObject.FileTypeEnum.Picture && !x.IsDelete);
            if (folderId.HasValue)
                query = query.Where(x => x.FolderId == folderId);
            return FindListAsync(query, pagination);
        }
        /// <summary>
        /// 文件夹分组统计
        /// </summary>
        /// <returns></returns>
        public Task<List<OssFolderGroupEntity>> GetFolderGroup()
        {
            return _dbSet.GroupBy(x => x.FolderId).Select(x => new OssFolderGroupEntity() { FolderId = x.Key, Number = x.Count() }).ToListAsync();
        }
        /// <summary>
        /// 获取全部文件夹
        /// </summary>
        /// <returns></returns>
        public Task<List<OssFolderEntity>> GetAllFolder()
        {
            return OssFolderDbSet.OrderBy(x => x.AddTime).ToListAsync();
        }

        public Task<List<OssFileEntity>> GetByIds(IList<Guid> fileIds)
        {
            return _dbSet.Where(x => fileIds.Contains(x.Id)).ToListAsync();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="entity"></param>
        public void AddFolder(OssFolderEntity entity)
        {
            OssFolderDbSet.Add(entity);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证文件夹名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ExistFolderName(string name)
        {
            return OssFolderDbSet.FirstOrDefault(x => x.Name == name) != null;
        }
        /// <summary>
        /// 验证文件夹编码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ExistFolderCode(string code)
        {
            return OssFolderDbSet.FirstOrDefault(x => x.Code == code) != null;
        }
        #endregion
    }
}
