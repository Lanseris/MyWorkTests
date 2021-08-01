using System;
using System.Collections.Generic;
using System.Text;

namespace EnotherGitTest.EventHandlers
{
    public class ClassVsDelegateAndEH
    {
        public delegate void SomeDelegate();

        public event SomeDelegate SomeDelegateEH;

        public void Print()
        {
            Console.WriteLine("Текст из метода, который был добавлен в event (сюда SomeDelegateEH) из вне:");
            //оповещаем всех, кто подписался
            //(вызываем методы, которые туда добавлены)
            SomeDelegateEH?.Invoke();
        }
    }
}
