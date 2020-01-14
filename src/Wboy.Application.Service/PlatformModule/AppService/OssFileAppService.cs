using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Application.Dto.PlatformModule;
using Wboy.Application.Service.PlatformModule.IAppService;
using Wboy.Domain.PlatformModule.Entities;
using Wboy.Domain.PlatformModule.IRepository;
using Wboy.Infrastructure.Core;
using Wboy.Infrastructure.Core.Util.WebControl;

namespace Wboy.Application.Service.PlatformModule.AppService
{
    public class OssFileAppService : IOssFileAppService
    {
        #region 基础属性
        private readonly IOssFileRepository _ossFileRepository;
        public OssFileAppService(IOssFileRepository ossFileRepository)
        {
            _ossFileRepository = ossFileRepository;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取全部文件夹
        /// </summary>
        /// <returns></returns>
        public async Task<List<OssFolderDto>> GetAllFolder()
        {
            var list = _ossFileRepository.GetAllFolder();
            var group = _ossFileRepository.GetFolderGroup();
            await Task.WhenAll(list, group);
            var result = list.Result.Select(x => new OssFolderDto(x, group.Result.FirstOrDefault(r => r.FolderId.Value == x.Id)?.Number ?? 0)).ToList();
            return result;
        }
        /// <summary>
        /// 可用图片列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        public async Task<List<OssFileLiteDto>> GetEnablePageList(Pagination pagination, Guid? folderId)
        {
            var list = await _ossFileRepository.GetEnablePicturePageList(pagination, folderId);
            return list.Select(x => new OssFileLiteDto(x)).ToList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> AddFolder(string name, string code,Guid loginUserId)
        {
            if (_ossFileRepository.ExistFolderName(name))
                throw new NeedToShowFrontException("文件夹名称已经存在");
            if (_ossFileRepository.ExistFolderCode(code))
                throw new NeedToShowFrontException("文件夹编码已经存在");
            var entity = new OssFolderEntity()
            {
                Id = Domain.NewId.NewSecuentialGuid(),
                Name = name,
                Code = code,
                AddUserId = loginUserId,
                AddTime = DateTime.Now
            };
            _ossFileRepository.AddFolder(entity);
            return await _ossFileRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns></returns>
        public async Task<bool> AddFile(Guid loginUserId, Guid? folderId, string fileName, string savePath, int fileSize, int fileType, string fileExtension)
        {
            var entity = new OssFileEntity()
            {
                Id = Domain.NewId.NewSecuentialGuid(),
                FileName = fileName,
                SavePath = savePath,
                FileSize = fileSize,
                FileType = (Domain.ValueObject.FileTypeEnum)fileType,
                FileExtension = fileExtension,
                FolderId = folderId,
                IsDelete = false,
                AddUserId = loginUserId,
                AddTime = DateTime.Now
            };
            _ossFileRepository.Insert(entity);
            return await _ossFileRepository.UnitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteFileByIds(IList<Guid> fileIds)
        {
            var list = await _ossFileRepository.GetByIds(fileIds);
            _ossFileRepository.Delete(list);
            return await _ossFileRepository.UnitOfWork.CommitAsync() > 0;
        }
        #endregion
    }
}
