using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Message
{
    public interface ISmsSender
    {
        /// <summary>
        /// 发送短信验证码 缓存用phone作为key
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        string SendSmsCode(string phone);
        /// <summary>
        /// 发送短信验证码 指定cacheKey
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        string SendSmsCode(string phone, string cacheKey);

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="code"></param>
        /// <param name="deleteKey">成功后删除key 默认 true</param>
        /// <returns></returns>
        bool ValidateSmsCode(string cacheKey, string code,bool deleteKey=true);
        /// <summary>
        ///  获取短信验证码
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        string GetSmsCode(string cacheKey);
        //void SendSmsMsg(string phone, string templateCode, string paramJsonString);
    }
}
