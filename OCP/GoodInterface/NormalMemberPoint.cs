using OCP.GoodInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.GoodInterface
{
    public sealed class NormalMemberPoint : IPoint
    {
        public int GetPoint(int price)
        {
            return (int)(price * 0.01);
        }
    }
}
