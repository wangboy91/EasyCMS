using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core
{
    public class NeedToShowFrontException : Exception
    {
        public NeedToShowFrontException(string message) :
            base(message)
        {

        }
    }
}
