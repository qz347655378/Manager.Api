using System.Collections.Generic;

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
