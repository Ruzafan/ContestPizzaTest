using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            Dictionary<long, int> values = new Dictionary<long, int>();
            int qtt = 0;
            foreach (var item in _sliceQtt)
            {
                values.Add(qtt, item);
                qtt++;
            }
            values = values.OrderBy(q => q.Value).ToDictionary(x => x.Key, x => x.Value);
            Dictionary<long, int> result = new Dictionary<long, int>();
            for (long i = _diferentTypesOfPizza-1; i >= 0; i--)
            {
                GetNext(values[i], result,i);
            }

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "result.txt")))
            {
                outputFile.WriteLine(result.Count());
                outputFile.WriteLine(string.Join(" ", result.Keys));
            }
        }

        private static void GetNext(int v, Dictionary<long, int> result,long currPos)
        {
            long slices = result.Values.Sum();
            if (slices < _maxiumSlices)
            {
                result.Add(currPos, v);
            }

            if(result.Values.Sum() > _maxiumSlices)
            {
                result.Remove(currPos);
            }
            
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
            _sliceQtt = new int[_diferentTypesOfPizza];
            string sliceqtt = string.Empty;
            foreach (char c in line)
            {
                if (c != ' ' && c != '\n')
                {
                    sliceqtt += c;
                }
                else if (c != '\n')
                {
                    int.TryParse(sliceqtt, out qtt);
                    _sliceQtt[count] = qtt;
                    sliceqtt = string.Empty;
                    count++;
                }
            }
            int.TryParse(sliceqtt, out qtt);
            _sliceQtt[count] = qtt;
            sliceqtt = string.Empty;

        }
    }
}
