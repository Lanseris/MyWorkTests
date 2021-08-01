using System;
using System.Threading;
using EnotherGitTest.asyncTests;
using EnotherGitTest.Delegate;
using EnotherGitTest.XmlSerializerTests;
using EnotherGitTest.EventHandlers;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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

            Console.WriteLine("5 - проверка регулярок");

            Console.WriteLine("6 - сериализатор");

            Console.WriteLine("7 - EventHeandlers");

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
                        #region проверка регулярок
                        string testString = "24X2234хsdfg23sdfg444444";

                        Regex rx = new Regex(@"\d{1,5}[x|х]\d{1,5}[x|х]\d{1,5}",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

                        var result = rx.Match(testString);
                        Console.WriteLine(result);
                        #endregion
                        break;
                    case 6:
                        #region Serializer
                        SerializeManager sm = new SerializeManager();
                        sm.Notify += (x) => { Console.WriteLine(x); };
                        sm.TrySerialize();
                        sm.TryDeserialize();
                        #endregion
                        break;
                    case 7:
                        #region EvendHandlers
                        ClassVsDelegateAndEH classVsDelegateAndEH = new ClassVsDelegateAndEH();

                        Counter counter = new Counter(classVsDelegateAndEH);

                        classVsDelegateAndEH.Print();
                        classVsDelegateAndEH.Print();
                        classVsDelegateAndEH.Print();
                        classVsDelegateAndEH.Print();
                        classVsDelegateAndEH.Print();
                        #endregion
                        break;
                    default:
                        break;
                }
            }


            Console.Read();
        }

        /// <summary>
        /// мерж словарей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="firstDictionary"></param>
        /// <param name="secondDictionary"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static Dictionary<T,E> DictionaryМerge<T, E>(Dictionary<T, E> firstDictionary, Dictionary<T, E> secondDictionary, Func<IEnumerable<KeyValuePair<T, E>>, E> resultSelector)
        {
            var concatinatedDic = firstDictionary.Concat(secondDictionary).GroupBy(x => x.Key, (firstType, secondType) => new { Key = firstType, Count = resultSelector(secondType) }).ToDictionary(k => k.Key,e=>e.Count);
            return concatinatedDic;
        }

        /// <summary>
        /// не нужны никакие ивенты
        /// </summary>
        public class TemplatePart
        {
            public bool _reading = false;

            public string StartString;


            public string EndString;

            string _path = @"C:\ProgramData\InduSoft\I-LDS\2\Log\IndusoftTestLog.log";

            public void Test()
            {
                using (StreamReader streamReader = new StreamReader(_path))
                {
                    while (!streamReader.EndOfStream)
                    {
                        LineProcessing(streamReader.ReadLine());
                    }
                }
            }

            public void LineProcessing(string fileRow)
            {
                bool result = CheckConditionMatch(fileRow);

                if (result)
                {
                    _reading = !_reading;
                }
              
                Console.WriteLine(fileRow);
                Console.Write("Result: "+result);
                Console.WriteLine(" Is_Reading: "+_reading);
                Console.WriteLine();
            }

            public bool CheckConditionMatch(string stringForCheck)
            {
                if (_reading)
                {
                    if (!string.IsNullOrWhiteSpace(EndString))
                    {
                        if (stringForCheck.Contains(EndString))
                        {

                            return true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(stringForCheck))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (stringForCheck.Contains(StartString))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
