using System;

namespace Wboy.Application.Dto
{
    public class BasicDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }

    public class BaseDto: BasicDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid AddUserId { get; set; }
        /// <summary>
        /// 编辑用户Id
        /// </summary>
        public Guid? UpdateUserId { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
