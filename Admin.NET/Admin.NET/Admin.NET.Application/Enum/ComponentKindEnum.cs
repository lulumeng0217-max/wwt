using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Enum;

public enum ComponentKindEnum
{
    /// <summary>
    /// 
    /// </summary>
    [Description("Container")]
    Container = 10,
    [Description("Layout")]
    Layout = 20,
    [Description("Content")]
    Content = 30,
}
