using System;
using System.Threading;
using EnotherGitTest.asyncTests;
using EnotherGitTest.Delegate;

namespace EnotherGitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер необходимой операции");

            Console.WriteLine("0 - Выход");

            Console.WriteLine("1 - очистка ");

            Console.WriteLine("2 - тесты рандомного кода");

            Console.WriteLine("3 - проверка асинхронных методов");

            Console.WriteLine("4 - проверка делегатов");

            Console.Write("Номер:");

            if(Int32.TryParse(Console.ReadLine(),out int operationNumber))
            {
                switch (operationNumber)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.Clear();
                        break;
                    case 2:
                        #region ТЕСТЫ РАНДОМНОГО КОДА
                        #endregion
                        break;
                    case 3:
                        #region AsyncMeethodTests
                        AsyncTestsClass.ReadWriteAsync();
                        Console.WriteLine("Некоторая работа");

                        var task = AsyncTestsClass.FactorialAsyncSequential();
                        task.Wait();//ожидание выполнения асинхронного метода 
                        AsyncTestsClass.PrintIEnumerableT(task.Result);

                        var task2 = AsyncTestsClass.FactorialAsyncParallel();
                        task2.Wait();
                        AsyncTestsClass.PrintIEnumerableT(task2.Result);

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

                        //тестирование остановки ассинхронного метода
                        Console.WriteLine();
                        Console.WriteLine("Остановка задачи");
                        CancellationTokenSource cts = new CancellationTokenSource();
                        CancellationToken token = cts.Token;
                        AsyncTestsClass.FactorialAsyncCansel(6, token);
                        Thread.Sleep(3000);
                        cts.Cancel();
                        Console.WriteLine("без остановки, выводил бы до 6-ти");

                        #endregion
                        break;
                    case 4:
                        #region DelegateTests

                        DelegateTests.Greetings();
                        Console.WriteLine();

                        DelegateTests.MathOperationsTest(2,4);
                        Console.WriteLine();


                        #endregion
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }


            Console.Read();
        }
    }
}
