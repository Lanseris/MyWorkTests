using System;
using System.Collections.Generic;
using System.Text;

namespace EnotherGitTest.Delegate
{
    public static class DelegateTests
    {
        public delegate int MathOperations(int x, int y);

        public delegate T MathOperationGeneralized<T, K>(K val);

        public delegate void Message();

        //мутк с вычисляющим делегатом
        public static void MathOperationsTest(int x, int y)
        {
            MathOperations mathOperations;

            Console.WriteLine("Сумма");
            mathOperations = Sum;
            Console.WriteLine(mathOperations(x,y));

            Console.WriteLine("Произведение");
            mathOperations += Multiply;
            Console.WriteLine(mathOperations?.Invoke(x,y));
            //вернётся значение последнего метода, возвращающего значение

            Console.WriteLine();
            Console.WriteLine("Обобщённый делегат");
            MathOperationGeneralized<decimal, int> op = Square;
            Console.WriteLine(op?.Invoke(5));

        }

        //мутки с текстовым делегатом
        public static void Greetings()
        {
            Message msg = Hello;
            msg += HowAreYou;
            msg();

            Console.WriteLine();
            Message msg2 = HowAreYou;

            Console.WriteLine();
            Message msg3 = msg + msg2;
            msg3();

            Console.WriteLine();
            msg3 -= msg2;
            msg3();

            Console.WriteLine();
            Console.WriteLine("Делегат в качестве передаваемого параметра");
            Show_Message(Hello);
        }

        #region Математические делегаты вида int (int,int)

        private static int Sum(int x, int y) => x + y;

        private static int Multiply(int x, int y) => x * y;

        private static decimal Square(int n) => n * n;

        #endregion

        #region Методы для текстового делегата

        private static void Hello()
        {
            Console.WriteLine("Hello");
        }
        private static void HowAreYou()
        {
            Console.WriteLine("How are you?");
        } 
        private static void Show_Message(Message _del)
        {
            _del?.Invoke();
        }

        #endregion
    }
}
