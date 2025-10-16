using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6modullaba
{

    public interface IShippingStrategy
    {
        decimal CalculateShippingPrice(decimal weigth, int distance);
    }

    public class StandardShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingPrice(decimal weigth, int distance)
        {
            return weigth * 0.9m + distance * 0.1m;
        }
    }
    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingPrice(decimal weigth, int distance)
        {
            return (weigth * 0.9m + distance * 0.1m) * 1.2m;
        }
    }
    public class InternationalShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingPrice(decimal weigth, int distance)
        {
            return (weigth * 0.9m + distance * 0.1m) + 10m;
        }
    }
    public class DeliveryContext
    {
        private IShippingStrategy _strategy;
        public void SetCostStrategy(IShippingStrategy strategy)
        {
            _strategy = strategy;
        }
        public decimal GetShippingCost(decimal weigth, int distance)
        {
            if (_strategy == null)
                throw new Exception("Strategy not selected");
            else
                return _strategy.CalculateShippingPrice(weigth, distance);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select shipping type: 1 - Standard, 2 - Express, 3 - International");
            string shipType = Console.ReadLine();


            DeliveryContext context = new DeliveryContext();
            switch (shipType)
            {
                case "1":
                    context.SetCostStrategy(new StandardShippingStrategy());
                    break;
                case "2":
                    context.SetCostStrategy(new ExpressShippingStrategy());
                    break;
                case "3":
                    context.SetCostStrategy(new InternationalShippingStrategy());
                    break;
            }

            Console.WriteLine("Weigth:");
            decimal weigth = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Distance:");
            int distance = Convert.ToInt32(Console.ReadLine());

            if (distance < 0 && weigth < 0)
                throw new Exception("Incorrect distance");

            decimal cost = context.GetShippingCost(weigth, distance);
            Console.WriteLine($"Total: {cost}");

        }
    }
}
