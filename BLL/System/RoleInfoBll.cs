using System.Linq;
using System.Threading.Tasks;
using IBLL.System;
using IDAL;
using Models.System;
using NotImplementedException = System.NotImplementedException;

namespace BLL.System
{
    public class RoleInfoBll : BaseBll<RoleInfo>, IRoleInfoBLl
    {
        public RoleInfoBll(IBaseDal<RoleInfo> currentDal) : base(currentDal)
        {
        }


    }
}
