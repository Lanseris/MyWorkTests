using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnotherGitTest.asyncTests
{
    public class AsyncTestsClass
    {
        public static async void ReadWriteAsync()
        {
            string s = "Hello world! One step at a time";

            // hello.txt - файл, который будет записываться и считываться
            using (StreamWriter writer = new StreamWriter("hello.txt", false))
            {
                await writer.WriteLineAsync(s);  // асинхронная запись в файл
            }
            using (StreamReader reader = new StreamReader("hello.txt"))
            {
                string result = await reader.ReadToEndAsync();  // асинхронное чтение из файла
                Console.WriteLine(result);
            }
        }

        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал числа {n} равен {result}");
        }

        //запуск задачь последовательно
        public static async Task FactorialAsyncSequential()
        {
            Console.WriteLine();
            Console.WriteLine("Последовательное выполнение асинхронных методов");
            await Task.Run(() => Factorial(3));
            await Task.Run(() => Factorial(4));
            await Task.Run(() => Factorial(5));
            await Task.Run(() => Factorial(6));
            await Task.Run(() => Factorial(7));
            await Task.Run(() => Factorial(8));

        }

        //запуск задачь параллельно
        //отслеживание выполнения трёх асинхронных методов одновременно
        public static async void FactorialAsyncParallel()
        {
            Console.WriteLine();
            Console.WriteLine("Параллельное выполлнение ассинхронных методов");
            Task t1 = Task.Run(() => Factorial(3));
            Task t2 = Task.Run(() => Factorial(4));
            Task t3 = Task.Run(() => Factorial(5));
            Task t4 = Task.Run(() => Factorial(6));
            Task t5 = Task.Run(() => Factorial(7));
            Task t6 = Task.Run(() => Factorial(8));

            //отслеживает завершение параллельно запущенных асинхронных методов
            //создаёт новую задачу, которая будет выполнена после завершения 
            //всех предоставленных задач.
            await Task.WhenAll(new[] { t1, t2, t3, t4, t5, t6 });

        }

    }
}
