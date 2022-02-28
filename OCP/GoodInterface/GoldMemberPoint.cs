using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.GoodInterface
{
    public sealed class GoldMemberPoint : IPoint
    {
        public int GetPoint(int point)
        {
            return (int)(point * 0.03);
        }
    }
}
