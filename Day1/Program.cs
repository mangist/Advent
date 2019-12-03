using System;
using System.IO;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            decimal fuelCounter = 0;

            Console.WriteLine("Which part (1, 2 or 3)?");
            var part = Console.ReadKey();

            if (part.KeyChar == '1')
            {
                foreach (var i in input)
                {
                    var mass = decimal.Parse(i);
                    fuelCounter += (Math.Floor(mass / 3) - 2);
                }
                Console.WriteLine($"Fuel counter is {fuelCounter}");
            }
            else if (part.KeyChar == '2')
            {
                foreach (var i in input)
                {
                    var mass = decimal.Parse(i);
                    decimal fuel = 0;
                    do
                    {
                        fuel = (Math.Floor(mass / 3) - 2);
                        fuelCounter += Math.Max(0, fuel);

                        mass = fuel;

                    } while (fuel > 0);
                }
                Console.WriteLine($"Fuel counter is {fuelCounter}");
            }

            Console.Read();

        }
    }
}
