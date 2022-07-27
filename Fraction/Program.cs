using System;
using System.Reflection.Metadata.Ecma335;
using Serilog;

namespace FractionNameSpace
{
    public class Program
    {
        public static void Main()
        {
            string logFile = @"log/myFraction.txt";
            var log = new LoggerConfiguration()
                       .MinimumLevel.Debug().WriteTo.File(logFile, rollingInterval: RollingInterval.Day).WriteTo
                       .Console().CreateLogger();

            
            Console.WriteLine("Введите значения первой дроби: ");
            
            Fraction f1 = new Fraction(
                integer: Int32.Parse(Console.ReadLine()),
                numerator: Int32.Parse(Console.ReadLine()),
                denominator: Int32.Parse(Console.ReadLine()));
            
            Console.WriteLine("Введите значения второй дроби: ");
            
            Fraction f2 = new Fraction(
                integer: Int32.Parse(Console.ReadLine()),
                numerator: Int32.Parse(Console.ReadLine()),
                denominator: Int32.Parse(Console.ReadLine()));
            
            
            Console.WriteLine(f1.Reduce().ToString());
            Console.WriteLine(f2.Reduce().ToString());

            if (f1 == f2)
            {
                Console.WriteLine($"Дробь {f1.ToString()} равна {f2.ToString()}");
            }
            else if (f1 > f2)
            {
                Console.WriteLine($"Дробь {f1.ToString()} больше {f2.ToString()}");
            }
            else if (f1 < f2)
            {
                Console.WriteLine($"Дробь {f1.ToString()} меньше {f2.ToString()}");
            }
            else
            {
                Console.WriteLine($"Дробь {f1.ToString()} не равна {f2.ToString()}");
            }
            
            
        }
    }
}