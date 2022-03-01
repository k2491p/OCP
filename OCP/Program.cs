using OCP.Factories;
using OCP.GoodAbstract;
using OCP.GoodInterface;
using System;

namespace OCP
{
    class Program
    {
        static void Main(string[] args)
        {
            var memberNo = args[0].ToString();
            int price = Convert.ToInt32(args[1]);

            // GoodInterfaceを動かす例
            IPoint point = Factory.CreateIPoint(memberNo);
            Console.WriteLine($"メンバー:{memberNo}は{price}円で{point.GetPoint(price)}ポイント獲得！");

            // GoodAbstractを動かす例
            PointBase point2 = Factory.CreatePointBase(memberNo);
            Console.WriteLine($"メンバー:{memberNo}は{price}円で{point2.GetPoint(price)}ポイント獲得！");
        }
    }
}
