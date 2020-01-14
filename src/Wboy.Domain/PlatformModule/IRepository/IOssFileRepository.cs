using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wboy.Domain.PlatformModule.Entities;
using Wboy.Infrastructure.Core.Util.WebControl;

namespace Wboy.Domain.PlatformModule.IRepository
{
    /// <summary>
    /// 文件 仓储接口
    /// </summary>
    public interface IOssFileRepository : IRepository<OssFileEntity>
    {
        #region 获取数据
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        Task<IEnumerable<OssFileEntity>> GetPageList(Pagination pagination, string keyword);
        /// <summary>
        /// 可用图片分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
       Task<IEnumerable<OssFileEntity>> GetEnablePicturePageList(Pagination pagination, Guid? folderId);
        /// <summary>
        /// 文件夹分组统计
        /// </summary>
        /// <returns></returns>
        Task<List<OssFolderGroupEntity>> GetFolderGroup();
        /// <summary>
        /// 获取全部文件夹
        /// </summary>
        /// <returns></returns>
        Task<List<OssFolderEntity>> GetAllFolder();
        Task<List<OssFileEntity>> GetByIds(IList<Guid> fileIds);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="entity"></param>
        void AddFolder(OssFolderEntity entity);
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证文件夹名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool ExistFolderName(string name);
        /// <summary>
        /// 验证文件夹编码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool ExistFolderCode(string code);
        #endregion
    }
}
