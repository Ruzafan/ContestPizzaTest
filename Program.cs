using System;
using System.Collections.Generic;
using System.IO;

namespace GoogleContestTest
{
    class Program
    {
        private static long _maxiumSlices = 0;
        private static long _diferentTypesOfPizza = 0;
        private static int[] _sliceQtt;

        static void Main(string[] args)
        {
            Console.WriteLine("App started, add the file path :)");
            string path = Console.ReadLine();
            ReadFileAndSetProps(path);
            Console.WriteLine("File readed.");


        }

        private static void ReadFileAndSetProps(string path)
        {
            string line;
            StreamReader file = new StreamReader(path);
            line = file.ReadLine();
            SetVarsLine1(line);
            line = file.ReadLine();
            SetVarsLine2(line);
            file.Close();






        }

        private static void SetVarsLine1(string line)
        {
            string MaxiumSlices = string.Empty;
            string TypesOfPizza = string.Empty;
            bool isSlices = true;

            foreach (char c in line)
            {
                if (c != ' ' && c != '\n')
                {
                    if (isSlices)
                    {
                        MaxiumSlices += c;
                    }
                    else
                    {
                        TypesOfPizza += c;
                    }
                }
                else if (c != '\n')
                {
                    isSlices = false;
                }
            }
            long.TryParse(MaxiumSlices, out _maxiumSlices);
            long.TryParse(TypesOfPizza, out _diferentTypesOfPizza);


        }
        private static void SetVarsLine2(string line)
        {
            int qtt = 0;
            int count = 0;
            _sliceQtt = new int[_maxiumSlices];
            string sliceqtt = string.Empty;
            foreach (char c in line)
            {
                if (c != ' ' && c != '\n')
                {
                    sliceqtt += c;
                }
                else if (c != '\n')
                {
                    qtt = 0;
                    int.TryParse(sliceqtt, out qtt);
                    _sliceQtt[count] = qtt;
                    sliceqtt = string.Empty;
                }
                count++;
            }

            qtt = 0;
            int.TryParse(sliceqtt, out qtt);
            _sliceQtt[count] = qtt;
            sliceqtt = string.Empty;

        }
    }
}
