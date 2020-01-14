using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CustomErrorModel
    {

        public int StatusCode { get; set; }
        public string RequestId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
