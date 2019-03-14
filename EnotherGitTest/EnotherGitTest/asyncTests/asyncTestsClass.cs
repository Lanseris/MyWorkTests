using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnotherGitTest.asyncTests
{
    public class AsyncTestsClass
    {
        public static void Factorial()
        {
            int result = 1;
            for (int i = 1; i <= 6; i++)
            {
                result *= i;
            }
            Thread.Sleep(8000);
            Console.WriteLine($"Факториал равен {result}");
        }

        // определение асинхронного метода
        public static async void FactorialAsync()
        {
            Console.WriteLine("Начало метода FactorialAsync"); // выполняется синхронно
            await Task.Run(() => Factorial());                            // выполняется асинхронно
            Console.WriteLine("Конец метода FactorialAsync");  // выполняется синхронно
        }

    }
}
