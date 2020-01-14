using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Extensions
{
    /// <summary>
    /// 结果状态
    /// </summary>
    public enum StateCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = -1,
        /// <summary>
        /// 没有数据
        /// </summary>
        NoData = 0,
    }
    /// <summary>
    /// 返回消息类
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public StateCode State { set; get; }
        /// <summary>
        /// 状态消息
        /// </summary>
        public string Message { set; get; }
        /// <summary>
        /// 默认实例化 默认失败 消息为空
        /// </summary>
        public ResultMessage()
            : this(StateCode.Fail, string.Empty)
        {
        }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="stateCode">状态码</param>
        /// <param name="stateMessage">状态消息</param>
        public ResultMessage(StateCode stateCode, string stateMessage)
        {
            State = stateCode;
            Message = stateMessage;
        }
    }
    /// <summary>
    /// 返回消息 泛型类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class ResultMessage<T> : ResultMessage
    {
        /// <summary>
        /// 数据对象
        /// </summary>
        public T Data { set; get; }
        /// <summary>
        /// 成功结果实例化
        /// </summary>
        /// <param name="data">泛型数据</param>
        public ResultMessage(T data)
            : this(StateCode.Success, "OK", data)
        {
        }
        /// <summary>
        /// 成功结果实例化
        /// </summary>
        /// <param name="data">泛型数据</param>
        /// <param name="successTip">成功提示</param>
        public ResultMessage(T data, string successTip)
            : this(StateCode.Success, successTip, data)
        {
        }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="stateMessage"></param>
        public ResultMessage(StateCode stateCode, string stateMessage)
            : base(stateCode, stateMessage)
        {

        }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="stateMessage"></param>
        /// <param name="data"></param>
        public ResultMessage(StateCode stateCode, string stateMessage, T data)
        {
            State = stateCode;
            Message = stateMessage;
            Data = data;
        }
    }
}
