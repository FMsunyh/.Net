using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fly.General
{
    /// <summary>
    /// 角色的方向
    /// </summary>
    public enum RolesDirection
    {
        L, LU, U, RU, R, RD, D, LD, STOP
    }

    /// <summary>
    /// 子弹的方向
    /// </summary>
    public enum MissileDirection
    { 
        L,LU,LUU,U,RUU,RU,R,RDD,RD,D,LDD,LD,STOP
    }
}
