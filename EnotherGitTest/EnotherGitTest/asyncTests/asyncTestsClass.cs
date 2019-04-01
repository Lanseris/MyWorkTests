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

        //вычисление факториалас добавлением лишней ассинхронности (для отлова ексепшенов)
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
        public static async Task<List<int>> FactorialAsyncSequential()
        {
            Console.WriteLine();
            Console.WriteLine("Последовательное выполнение асинхронных методов");

            List<int> asyncConsistently = new List<int>();
            
            await Task.Run(() => asyncConsistently.Add(Factorial(3).Result));
            await Task.Run(() => asyncConsistently.Add(Factorial(4).Result));
            await Task.Run(() => asyncConsistently.Add(Factorial(5).Result));
            await Task.Run(() => asyncConsistently.Add(Factorial(6).Result));
            await Task.Run(() => asyncConsistently.Add(Factorial(7).Result));
            await Task.Run(() => asyncConsistently.Add(Factorial(8).Result));

            return asyncConsistently;

            //так наглядней чем в цикле,чтобы лишних сомнений не было,
            //что пока одна ассинхронначя операция не отработает,
            //следующая не запустится 
        }

        //запуск задачь параллельно
        //отслеживание выполнения шести асинхронных методов одновременно
        public static async Task<List<int>> FactorialAsyncParallel()
        {
            Console.WriteLine();
            Console.WriteLine("Параллельное выполлнение ассинхронных методов");

            List<int> asyncParallel = new List<int>();

            Task t1 = Task.Run(() => asyncParallel.Add(Factorial(3).Result));
            Task t2 = Task.Run(() => asyncParallel.Add(Factorial(4).Result));
            Task t3 = Task.Run(() => asyncParallel.Add(Factorial(5).Result));
            Task t4 = Task.Run(() => asyncParallel.Add(Factorial(6).Result));
            Task t5 = Task.Run(() => asyncParallel.Add(Factorial(7).Result));
            Task t6 = Task.Run(() => asyncParallel.Add(Factorial(8).Result));

            //отслеживает завершение параллельно запущенных асинхронных методов
            //создаёт новую задачу, которая будет выполнена после завершения 
            //всех предоставленных задач.
            await Task.WhenAll(new[] { t1, t2, t3, t4, t5, t6 });

            return asyncParallel;
        }

        //отлавливание одной ошибки
        public static async Task FactorialAsyncTryCatch(int n)
        {
            try
            {
                Task<int> t1 = Factorial(n); //Task.Run(() => Factorial(n)); //(вариант. если вызываемый метод неассинхронный)
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

        //отлавливание нескольких ошибок (2 ошибки из трёх вызовов)
        public static async Task MultFactorialAsyncTryCatch()
        {
            Task allTasks = null;

            try
            {
                Task t1 = Factorial(-3);
                Task t2 = Factorial(6);
                Task t3 = Factorial(-10);

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

        public static async Task FactorialAsyncCansel(int n)
        {
            try
            {
                Task<int> t1 = Factorial(n);
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


        #region auxiliary_methods
        public static void PrintIEnumerableT<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item.ToString());
            }
        }
        #endregion
    }
}
