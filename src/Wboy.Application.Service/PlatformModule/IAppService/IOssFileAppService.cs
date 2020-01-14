using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wboy.Application.Dto.PlatformModule;
using Wboy.Infrastructure.Core.Util.WebControl;

namespace Wboy.Application.Service.PlatformModule.IAppService
{
    public interface IOssFileAppService
    {
        #region 获取数据
        /// <summary>
        /// 获取全部文件夹
        /// </summary>
        /// <returns></returns>
       Task<List<OssFolderDto>> GetAllFolder();
        /// <summary>
        /// 可用图片列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        Task<List<OssFileLiteDto>> GetEnablePageList(Pagination pagination, Guid? folderId);
        #endregion

        #region 提交数据

        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        Task<bool> AddFolder(string name, string code,Guid loginUserId);

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <param name="folderId">文件夹id</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns></returns>
        Task<bool> AddFile(Guid loginUserId, Guid? folderId, string fileName, string savePath, int fileSize, int fileType, string fileExtension);
       Task<bool> DeleteFileByIds(IList<Guid> fileIds);
        #endregion
    }
}
