using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.GoodAbstract
{
    sealed class GoldMemberPoint : PointBase
    {
        protected override int GetPointSub(int price)
        {
            return (int)(price * 0.03);
        }
    }
}
