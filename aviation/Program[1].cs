using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> testList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                testList.Add(i);
            }
            printList(testList);
            testList.Insert(1, 99);
            printList(testList);
            Console.ReadLine();
        }

        private static void printList(List<int> testList)
        {
            foreach (int i in testList)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }
    }
}
