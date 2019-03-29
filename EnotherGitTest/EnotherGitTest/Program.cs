using System;
using EnotherGitTest.asyncTests;

namespace EnotherGitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер необходимой операции");

            Console.WriteLine("1 - проверка асинхронных методов");

            Console.Write("Номер:");

            if(Int32.TryParse(Console.ReadLine(),out int operationNumber))
            {
                switch (operationNumber)
                {
                    case 1:
                        #region AsyncMeethodTests
                        AsyncTestsClass.ReadWriteAsync();
                        Console.WriteLine("Некоторая работа");

                        var task = AsyncTestsClass.FactorialAsyncSequential();
                        task.Wait();//ожидание выполнения асинхронного метода 

                        var task2 = AsyncTestsClass.FactorialAsyncParallel();
                        task2.Wait();

                        Console.WriteLine();

                        //отлавливание ошибок ассинхронного метода
                        Console.WriteLine("Отлавливание ошибок ассинхронного метода (с ошибкой)");
                        var task3 = AsyncTestsClass.FactorialAsyncTryCatch(-3);
                        task3.Wait();

                        Console.WriteLine();

                        Console.WriteLine("Отлавливание ошибок ассинхронного метода (без ошибки)");
                        var task4 = AsyncTestsClass.FactorialAsyncTryCatch(2);
                        task4.Wait();

                        Console.WriteLine("Отлавливание параллельных ошибок");
                        var task5 = AsyncTestsClass.MultFactorialAsyncTryCatch();
                        task5.Wait();
                        #endregion
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }


            Console.Read();
        }
    }
}
