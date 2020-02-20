using System;
using System.Collections.Generic;
using System.IO;

namespace GoogleContestFirstRound
{
    public class Program
    {
        public static long books { get; set; }
        public static long libraries { get; set; }
        public static long daysForScanning { get; set; }
        public static Dictionary<long,long> scoreOfBooks { get; set; }
        public static List<Library> libObjects { get; set; }

        static void Main(string[] args)
        {
            libObjects = new List<Library>();
            scoreOfBooks = new Dictionary<long, long>();
            Console.WriteLine("App started, add the file path :)");
            string fileNum = Console.ReadLine();
            string fileName = GetFile(fileNum);
            ReadFileAndSetProps(fileName);
        }

        private static string GetFile(string fileNum)
        {
            switch (fileNum)
            {
                case "1":
                    return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/a_example.txt";
            }
            return "";
        }

        private static void ReadFileAndSetProps(string path)
        {
            string line;
            StreamReader file = new StreamReader(path);
            line = file.ReadLine();
            SetVarsLine1(line);

            line = file.ReadLine();
            SetVarsScoreBooks(line);



            while ((line = file.ReadLine()) != null)
            {
                var libObj = new Library();
                SetVarOfLibrary(line, libObj);
                line = file.ReadLine();
                SetVarsOfBooks(line, libObj);
                libObjects.Add(libObj);
            }
            
            file.Close();
        }

        private static void SetVarsScoreBooks(string line)
        {
            string currentVal = string.Empty;
            long qtt = 0;
            long val = 0;
            foreach (char c in line)
            {
                if (c != ' ' && c != '\n')
                {
                    currentVal += c;
                }
                else if (c != '\n')
                {
                    val = long.Parse(currentVal);
                    scoreOfBooks.Add(qtt,val);
                    currentVal = "";
                    qtt++;
                }
            }
            val = long.Parse(currentVal);
            scoreOfBooks.Add(qtt, val);
        }

        private static void SetVarsOfBooks(string line, Library libObj)
        {
            string currentVal = string.Empty;
            long qtt = 0;
            long val = 0;
            foreach (char c in line)
            {
                if (c != ' ' && c != '\n')
                {
                    currentVal += c;
                }
                else if (c != '\n')
                {
                    val = long.Parse(currentVal);
                    libObj.Books.Add(qtt,val);
                    currentVal = "";
                    qtt++;
                }
            }
            val = long.Parse(currentVal);
            libObj.Books.Add(qtt, val);
        }

        private static void SetVarOfLibrary(string line, Library libObj)
        {
            int pos = 0;
            long[] info = new long[3];
            string currentVal = string.Empty;
            foreach (char c in line)
            {
                if (c != ' ' && c != '\n')
                {
                    currentVal += c;
                }
                else if (c != '\n')
                {
                    info[pos] = long.Parse(currentVal);
                    currentVal = "";
                    pos++;
                }
            }

            info[pos] = long.Parse(currentVal);

            libObj.NumOfBooks = info[0];
            libObj.SingUpProcesDays = info[1];
            libObj.BooksPerDay = info[2];
        }

        private static void SetVarsLine1(string line)
        {
            int pos = 0;
            long[] info = new long[3];
            string currentVal = string.Empty;
            foreach (char c in line)
            {
                if (c != ' ' && c != '\n')
                {
                    currentVal += c;
                }
                else if (c != '\n')
                {
                    info[pos] = long.Parse(currentVal);
                    pos++;
                    currentVal = string.Empty;
                }
            }
            
            info[pos] = long.Parse(currentVal);

            books = info[0];
            libraries = info[1];
            daysForScanning = info[2];
        }
    }
}

