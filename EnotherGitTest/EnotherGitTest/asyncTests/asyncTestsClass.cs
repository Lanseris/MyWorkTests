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

        static async Task<int> Factorial(int n)
        {
            if (n < 1)
                throw new Exception($"{n} : число не должно быть меньше 1");

            int result = 1;
            await Task.Run(()=>
            {
                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                }
            });
            return result;
        }

        //запуск задачь последовательно
        public static async Task FactorialAsyncSequential()
        {
            Console.WriteLine();
            Console.WriteLine("Последовательное выполнение асинхронных методов");
            await Task.Run(() => Console.WriteLine(Factorial(3).Result));
            await Task.Run(() => Factorial(4));
            await Task.Run(() => Factorial(5));
            await Task.Run(() => Factorial(6));
            await Task.Run(() => Factorial(7));
            await Task.Run(() => Factorial(8));

        }

        //запуск задачь параллельно
        //отслеживание выполнения трёх асинхронных методов одновременно
        public static async Task FactorialAsyncParallel()
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

        //отлавливание одной ошибки
        public static async Task FactorialAsyncTryCatch(int n)
        {
            try
            {
                Task<int> t1 = Task.Run(() => Factorial(n));
                await t1;
                Console.WriteLine(t1.Result);
            }
            catch (Exception ex)
            {
                await Task.Run(() => Console.WriteLine(ex.Message));
            }
            finally
            {
                await Task.Run(() => Console.WriteLine("await в блоке finalyy"));
                Console.WriteLine();
            }
        }

        //отлавливание нескольких ошибок
        public static async Task MultFactorialAsyncTryCatch()
        {
            Task allTasks = null;

            try
            {
                Task t1 = Task.Run(() => Factorial(-3));
                Task t2 = Task.Run(() => Factorial(6));
                Task t3 = Task.Run(() => Factorial(-10));

                allTasks = Task.WhenAll(t1, t2, t3);
                await allTasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение: "+ ex.Message);
                Console.WriteLine("IsFaulted: " + allTasks.IsFaulted);
                foreach (var inx in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("Внутреннее исключение: " + inx.Message);
                }
            }
        }

    }
}
