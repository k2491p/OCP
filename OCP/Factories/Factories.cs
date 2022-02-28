using OCP.GoodInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Factories
{
    class Factories
    {
        public static IPoint CreatePoint(string memberNo)
        {
            if (memberNo.StartsWith("G"))
            {
                return new GoldMemberPoint();
            }

            if (memberNo.StartsWith("S"))
            {
                return new SilverMemberPoint();
            }

            return new NormalMemberPoint();

        }
    }
}
