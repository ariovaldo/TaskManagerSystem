using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Domain.Base
{
    public class ApiError
    {
        public ApiError(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}
