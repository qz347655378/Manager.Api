using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel.Menu
{
    public class RoleActionViewModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 菜单ID集合
        /// </summary>
        public List<int> ActionIds { get; set; }
    }
}
