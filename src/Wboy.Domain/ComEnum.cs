using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Domain
{
    /// <summary>
    /// 用户性别
    /// </summary>
    public enum UserSex
    {
        /// <summary>
        /// 男士
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女士
        /// </summary>
        Woman = 2,
        //未知
        Unknow = 3
    }
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// Android
        /// </summary>
        Android = 1,
        /// <summary>
        /// ios
        /// </summary>
        Ios = 2
    }
    public enum EscrowType
    {
        /// <summary>
        /// 微信
        /// </summary>
        WeChat = 1,
        /// <summary>
        /// QQ
        /// </summary>
        QQ = 2,
        /// <summary>
        /// 微博
        /// </summary>
        Weibo = 3,
        /// <summary>
        /// 小米账号
        /// </summary>
        XiaoMi = 4

    }
}
