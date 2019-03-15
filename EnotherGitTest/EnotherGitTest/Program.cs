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
                        AsyncTestsClass.FactorialAsync();
                        #endregion
                        break;
                    default:
                        break;
                }
            }

            Console.Read();
        }
    }
}
