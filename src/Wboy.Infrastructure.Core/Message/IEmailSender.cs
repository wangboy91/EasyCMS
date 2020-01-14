using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Message
{
    public interface IEmailSender
    {
        void SendEmailCode(string emailAddress);
        bool ValidateEmailCode(string emailAddress, string code);
        string GetEmailCode(string emailAddress);

    }
}
