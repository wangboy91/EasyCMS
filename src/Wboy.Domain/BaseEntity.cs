using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Domain
{
    public abstract class BaseEntity : Entity
    {
        /// <summary>
        /// 添加用户Id
        /// </summary>
        public Guid AddUserId { get; set; }
        /// <summary>
        /// 编辑用户Id
        /// </summary>
        public Guid? UpdateUserId { get; set; }
        /// <summary>
        ///  编辑时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
