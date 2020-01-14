using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Wboy.Infrastructure.Core;
using Wboy.Infrastructure.Core.Extension;

namespace WebApp.Extensions
{
    public static class PictureOperate
    {
        private readonly static string[] _fileTypeArray = { ".png", ".jpeg", ".jpg", ".gif", ".webp" };
        private static bool CheckFileType(string fileName)
        {
            var extensionName = Path.GetExtension(fileName);
            return _fileTypeArray.Any(t => t.Equals(extensionName));
        }
        public static string SaveFile(IHostingEnvironment hostingEnv, string base64Data, string folder, string fileName)
        {
            if (!CheckFileType(fileName))
            {
                throw new NeedToShowFrontException("文件类型不合法");
            }
            base64Data = base64Data.Substring(base64Data.IndexOf(",") + 1);
            folder = folder.EndsWith("/") ? folder.Substring(0, folder.Length - 1) : folder;
            string savefolder = hostingEnv.WebRootPath + $@"\files\{folder.Replace("/", "\\")}\";
            //string savefolder = AppDomain.CurrentDomain.BaseDirectory + $@"\files\{folder.Replace("/", "\\")}\";
            //if (hostingEnv.IsDevelopment()) {
            //    savefolder = Directory.GetCurrentDirectory() + $@"\files\{folder.Replace("/", "\\")}\";
            //}
            if (!Directory.Exists(savefolder))
            {
                Directory.CreateDirectory(savefolder);
            }
            var extensionName = Path.GetExtension(fileName);
            //var newfileName = fileName.MD5Encryption();
            var newfileName = Guid.NewGuid().ToString().Replace("-", "");
            string filePath = savefolder + "\\" + newfileName + extensionName;
            var fileBytes = Convert.FromBase64String(base64Data.Replace(" ", "+"));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (Stream s = new FileStream(filePath, FileMode.Create))
            {
                s.Write(fileBytes, 0, fileBytes.Length);
                s.Close();
            }
            return "/files/" + folder + "/" + newfileName + extensionName;
        }
        public static string SaveFile(IHostingEnvironment hostingEnv, IFormFile saveFile, string folder)
        {

            if (!CheckFileType(saveFile.FileName))
            {
                throw new NeedToShowFrontException("文件类型不合法");
            }
            folder = folder.EndsWith("/") ? folder.Substring(0, folder.Length - 1) : folder;
            string savefolder = hostingEnv.WebRootPath + $@"\files\{folder.Replace("/", "\\")}\";
            //string savefolder = AppDomain.CurrentDomain.BaseDirectory + $@"\files\{folder.Replace("/", "\\")}\";
            //if (hostingEnv.IsDevelopment())
            //{
            //    savefolder = Directory.GetCurrentDirectory() + $@"\files\{folder.Replace("/", "\\")}\";
            //}
            if (!Directory.Exists(savefolder))
            {
                Directory.CreateDirectory(savefolder);
            }
            var extensionName = Path.GetExtension(saveFile.FileName);
            var newfileName = Guid.NewGuid().ToString().Replace("-", "");
            //var newfileName = saveFile.FileName.MD5Encryption();
            string filePath = savefolder + "\\" + newfileName + extensionName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                saveFile.CopyTo(fs);
                fs.Flush();
            }
            return "/files/" + folder +"/" + newfileName + extensionName;
        }
        //public Stream GetImage(string userName, string fileName, out string fileType)
        //{
        //    var filePath = DirectoryUtils.CreateFilePath("/images/" + userName, fileName);
        //    if (!File.Exists(filePath))
        //    {
        //        throw new NeedToShowFrontException("文件不存在");
        //    }
        //    var fs = new FileStream(filePath, FileAccess.Open);
        //    fileType = "image/" + GetFileTypeByName(fileName);
        //    return fs;

        //}
        //public void DeleteFile(string relativePath)
        //{
        //    var filePath = DirectoryUtils.GetAbsolutePath(relativePath);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //}
        //public void DeleteFile(string folder, string fileName)
        //{
        //    var filePath = DirectoryUtils.CreateFilePath("/images/" + folder, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //}

        public static void DeleteFiles(IHostingEnvironment hostingEnv,IList<string> filesPath)
        {
            foreach (var path in filesPath)
            {
                if (File.Exists(hostingEnv.WebRootPath + path))
                {
                    File.Delete(hostingEnv.WebRootPath + path);
                }
            }
        }

    }
}