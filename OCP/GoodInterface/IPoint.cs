using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.GoodInterface
{
    interface IPoint
    {
        protected enum memberType
        {
            NORMAL,
            SILVER,
            GOLD
        };

        public int GetPoint();
    }
}
