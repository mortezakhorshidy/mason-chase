using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelViews
{
    public class ResultModelView
    {
        public ResultTypeEnum Result { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Model { get; set; }
    }
    public enum ResultTypeEnum
    {
        Error = 0,
        Success = 1,
        Warning = 2,

    }
}
