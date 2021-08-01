using System;
using System.Collections.Generic;
using System.Text;

namespace EnotherGitTest.EventHandlers
{
    //класс, которому важно знать сколько раз вызывался метод Print 
    public class Counter
    {
        int count = 0;

        public Counter(ClassVsDelegateAndEH classVsDelegateAndEH)
        {
            //подписка на event в классе, количество вызовов метода которого нам важно
            //(добавили в event того класса метод из этого класса)
            classVsDelegateAndEH.SomeDelegateEH += CountUpdate;
        }

        public void CountUpdate()
        {
            count++;
            Console.WriteLine(count);
        }
    }
}
