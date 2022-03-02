using OCP.Factories;
using OCP.GoodInterface;
//using OCP.GoodAbstract;
//using OCP.GoodInterface;
using System;

namespace OCP
{
    class Program
    {
        static void Main(string[] args)
        {
            IPoint _point;
            var memberNo = args[0].ToString();
            int price = Convert.ToInt32(args[1]);

            // GoodInterfaceを動かす例
            //IPoint point = Factory.CreateIPoint(memberNo);
            //Console.WriteLine($"メンバー:{memberNo}は{price}円で{point.GetPoint(price)}ポイント獲得！");

            // GoodAbstractを動かす例
            //PointBase point2 = Factory.CreatePointBase(memberNo);
            //Console.WriteLine($"メンバー:{memberNo}は{price}円で{point2.GetPoint(price)}ポイント獲得！");

            // Badを動かす例
            if (memberNo.StartsWith("G"))
            {
                _point = new GoldMemberPoint();
                Console.WriteLine($"メンバー:{memberNo}は{price}円で{_point.GetPoint(price)}ポイント獲得！");
            }
            else if (memberNo.StartsWith("S"))
            {
                _point = new SilverMemberPoint();
                Console.WriteLine($"メンバー:{memberNo}は{price}円で{_point.GetPoint(price)}ポイント獲得！");
            }
            else
            {
                _point = new NormalMemberPoint();
                Console.WriteLine($"メンバー:{memberNo}は{price}円で{_point.GetPoint(price)}ポイント獲得！");

            }

        }
    }
}
