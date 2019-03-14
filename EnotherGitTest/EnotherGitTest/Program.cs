using System;
using EnotherGitTest.asyncTests;

namespace EnotherGitTest
{
    class Program
    {
        static void Main(string[] args)
        {

            #region AsyncMeethodTests
            AsyncTestsClass.ReadWriteAsync();
            Console.WriteLine("Некоторая работа");

            AsyncTestsClass.FactorialAsync();
            #endregion

            Console.Read();
        }
    }
}
