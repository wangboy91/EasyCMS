using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Wboy.Application.Service.PlatformModule.IAppService;
using Wboy.Infrastructure.Core;
using Wboy.Infrastructure.Core.AliLib;
using Wboy.Infrastructure.Core.Configuration;
using Wboy.Infrastructure.Core.Extension;
using Wboy.Infrastructure.Core.Security;
using Wboy.Infrastructure.Core.Util.WebControl;
using WebApp.Extensions;

namespace WebApp.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class OssFileController : BaseController
    {
        #region 基础属性
        private readonly IOssFileAppService _ossFileAppService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public OssFileController(IOssFileAppService ossFileAppService, IHostingEnvironment hostingEnvironment)
        {
            _ossFileAppService = ossFileAppService;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region 视图功能
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FolderForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 可用图片列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEnablePicturePageList(Pagination pagination, Guid? folderId)
        {
            try
            {
                var data = await _ossFileAppService.GetEnablePageList(pagination, folderId);
                var JsonData = new
                {
                    rows = data,
                    pagination.total,
                    pagination.page,
                    pagination.records
                };
                return ToJsonResult(JsonData);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 获取全部文件夹
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllFolder()
        {
            try
            {
                var data = await _ossFileAppService.GetAllFolder();
                return ToJsonResult(data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFolder(string name, string code)
        {
            try
            {
                var loginUserId = User.Identity.GetLoginUser().UserId;
                var isOk = await _ossFileAppService.AddFolder(name, code, loginUserId);
                return Success("创建成功");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public async Task<IActionResult> UploadImage(Guid? folderId, string folder)
        {
            var loginUserId = User.Identity.GetLoginUser().UserId;
            if (Request.Form.Files.Count != 1)
                return Error("请选择单个上传文件");
            var file = Request.Form.Files[0];
            //string[] fileTypeArray = { ".png", ".jpeg", ".jpg", ".gif" };
            //var extensionName = Path.GetExtension(file.FileName);
            //if (!fileTypeArray.Any(t => t.Equals(extensionName)))
            //    return Error("只支持png jpg gif格式图片");
            //var virtualPath = $"PGC/{folder}/";
            //var fileName = file.FileName.MD5Encryption() + extensionName;
            //var filePathKey = virtualPath + fileName;
            //var aliyunOssHelper = new AliyunOssHelper();
            //using (var stream = file.OpenReadStream())
            //{
            //    aliyunOssHelper.PutObject(filePathKey, stream);
            //}
            var path = PictureOperate.SaveFile(_hostingEnvironment, file, folder);


            //var imgHost = ConfigurationManager.GetSetting<SettingBase>().LoaclHost;
            //var imgHost = ConfigurationManager.GetSetting<SettingAliyun>().OssDomainUrl;
            //var fullPath = imgHost + path;
            var isOk = await _ossFileAppService.AddFile(loginUserId,folderId, file.FileName, path, (int)file.Length, 3, Path.GetExtension(file.FileName));
            return Success(path);
        }
        /// <summary>
        /// 保存base64图片
        /// </summary>
        /// <param name="base64Url"></param>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        public IActionResult SaveBase64Url(string base64Url, string extensionName, string folder, string fileName)
        {
            try
            {
                //string base64 = base64Url.Substring(base64Url.IndexOf(",") + 1);
                //byte[] bt = Convert.FromBase64String(base64);
                //MemoryStream stream = new MemoryStream(bt);
                //var virtualPath = $"PGC/{folder}/";
                //var newfileName = fileName.MD5Encryption() + extensionName;
                //var filePathKey = virtualPath + newfileName;
                //var aliyunOssHelper = new AliyunOssHelper();
                //aliyunOssHelper.PutObject(filePathKey, stream);
                var path = PictureOperate.SaveFile(_hostingEnvironment, base64Url, folder, fileName);
                //var imgHost = ConfigurationManager.GetSetting<SettingBase>().LoaclHost;
                return Success(path);
            }
            catch (NeedToShowFrontException ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(IList<Guid> ids, IList<string> filesPath)
        {
            try
            {
                PictureOperate.DeleteFiles(_hostingEnvironment, filesPath);
                var isOk = await _ossFileAppService.DeleteFileByIds(ids);
                return Success("删除成功");
            }
            catch (NeedToShowFrontException ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion
    }
}