using OCP.GoodInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Factories
{
    static class Factory
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
