using OCP.GoodAbstract;
using OCP.GoodInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Factories
{
    static class Factory
    {
        public static IPoint CreateIPoint(string memberNo)
        {
            if (memberNo.StartsWith("G"))
            {
                return new GoodInterface.GoldMemberPoint();
            }

            if (memberNo.StartsWith("S"))
            {
                return new GoodInterface.SilverMemberPoint();
            }

            return new GoodInterface.NormalMemberPoint();

        }

        public static PointBase CreatePointBase(string memberNo)
        {
            if (memberNo.StartsWith("G"))
            {
                return new GoodAbstract.GoldMemberPoint();
            }

            if (memberNo.StartsWith("S"))
            {
                return new GoodAbstract.SilverMemberPoint();
            }

            return new GoodAbstract.NormalMemberPoint();

        }
    }
}
