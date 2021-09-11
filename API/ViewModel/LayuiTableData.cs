using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{

    /// <summary>
    /// 表格数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableData<T>
    {
        public int TotalCount { get; set; }

        public int CurrentPage { get; set; }

        public List<T> List { get; set; }
    }
}
