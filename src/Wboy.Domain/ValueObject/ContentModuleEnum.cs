using System.ComponentModel;

namespace Wboy.Domain.ValueObject
{
    /// <summary>
    /// 赞对象枚举
    /// </summary>
    public enum FavourObjectEnum : byte
    {
        /// <summary>
        /// 文章
        /// </summary>
        [Description("文章")]
        Article = 1,
        /// <summary>
        /// 评论
        /// </summary>
        [Description("评论")]
        Comment = 2
    }
}
