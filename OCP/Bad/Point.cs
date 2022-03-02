using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Bad
{
    public sealed class Point
    {
        public int GetPoint(int price, string memberNo)
        {
            if (memberNo.StartsWith("G"))
            {
                return (int)(price * 0.03);
            }

            if (memberNo.StartsWith("S"))
            {
                return (int)(price * 0.02);
            }

            return (int)(price * 0.01);

        }
    }
}
