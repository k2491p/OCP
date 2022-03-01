using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.GoodAbstract
{
    abstract class PointBase
    {
        public int GetPoint(int price)
        {
            var point = GetPointSub(price);
            if (DateTime.Now.Day == 1)
            {
                point *= 10;
            }
            return point;
        }

        protected abstract int GetPointSub(int price);
    }
}
