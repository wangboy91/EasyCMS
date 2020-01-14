using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Domain.ValueObject
{
    /// <summary>
    /// 举报原因
    /// </summary>
    public enum TipoffsEnumReasonEnum
    {
        /// <summary>
        /// 色情低俗
        /// </summary>
        Pron = 1,
        /// <summary>
        /// 广告骚扰
        /// </summary>
        AdHarass = 2,
        /// <summary>
        /// 政治宗教
        /// </summary>
        PoliticsAndReligion = 3,
        /// <summary>
        /// 虚拟欺骗
        /// </summary>
        VirtualDeceive = 4,
        /// <summary>
        /// 侵权(肖像、诽谤、抄袭、冒用)
        /// </summary>
        Infringement = 5,
        /// <summary>
        /// 违法内容
        /// </summary>
        Illegal = 6,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 7
    }
    /// <summary>
    /// 举报处理状态
    /// </summary>
    public enum TipoffsEnumStatusEnum
    {
        /// <summary>
        /// 待审核
        /// </summary>
        NotHandle = 1,
        /// <summary>
        /// 已审核
        /// </summary>
        Handled = 2

    }
    /// <summary>
    /// 处理结果
    /// </summary>
    public enum TipoffsEnumResultEnum
    {
        /// <summary>
        /// 未处理
        /// </summary>
        NotHandle = 1,
        /// <summary>
        /// 删除整条内容
        /// </summary>
        DeleteAll = 2,
        /// <summary>
        /// 删除资源文件
        /// </summary>
        DeleteOssFiles = 3,
        /// <summary>
        /// 驳回
        /// </summary>
        Reject = 4
    }
}
