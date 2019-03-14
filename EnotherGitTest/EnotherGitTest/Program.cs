using System;
using EnotherGitTest.asyncTests;

namespace EnotherGitTest
{
    class Program
    {
        static void Main(string[] args)
        {

            #region AsyncMeethodTests

            AsyncTestsClass.FactorialAsync();   // вызов асинхронного метода
            Console.WriteLine("Введите число: ");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Квадрат числа равен {n * n}");

            #endregion


            Console.Read();
        }
    }
}
