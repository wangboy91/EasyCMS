using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Message
{
    public class VerificationCode
    {
        public string Code { set; get; }
        public DateTime SendTime { set; get; }
    }
}
