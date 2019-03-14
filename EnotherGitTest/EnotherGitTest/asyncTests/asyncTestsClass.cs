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

    }
}
