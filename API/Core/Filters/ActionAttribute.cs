using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Filters
{
    /// <summary>
    /// 指定验证属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ActionAttribute : Attribute
    {
        public string Code { get; set; }

        public ActionAttribute(string code)
        {
            Code = code;
        }

    }


}
