using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoogleContestFirstRound
{
    public class Program
    {
        public static List<string> sources = new List<string>
        {
            "a_example",
            "b_read_on",
            "c_incunabula",
            "d_tough_choices",
            "e_so_many_books",
            "f_libraries_of_the_world"
        };

        public static long books { get; set; }
        public static long libraries { get; set; }
        public static long daysForScanning { get; set; }
        public static Dictionary<long, long> scoreOfBooks { get; set; }
        public static List<Library> libObjects { get; set; }

        static void Main(string[] args)
        {
            foreach (var item in sources)
            {
                libObjects = new List<Library>();
                scoreOfBooks = new Dictionary<long, long>();

                ReadFileAndSetProps(Path.Combine(Environment.CurrentDirectory, "HashCode", $"{item}.txt"));

                //var test1 = Test1();
                //WriteTest($"{item}_solution1.txt", test1);

                //var test2 = Test2();
                //WriteTest($"{item}_solution2.txt", test2);

                var test3 = Test3();
                WriteTest($"{item}_solution3.txt", test3);
            }
        }

        private static void WriteTest(string path, string text)
        {
            string docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "HashCode");
            if (!Directory.Exists(docPath))
                Directory.CreateDirectory(docPath);
            File.WriteAllText(Path.Combine(docPath, path), text);
        }

        private static string Test1()
        {
            var sb = new StringBuilder();

            var booksReaded = new List<long>();

            sb.AppendLine(libObjects.Count().ToString());

            var librariesOrdered = libObjects.OrderByDescending(x => (x.BooksPerDay - x.SingUpProcesDays));

            foreach (var library in librariesOrdered)
            {
                library.Books = library.Books.OrderBy(x => new Random().Next(0, library.Books.Count())).ToDictionary(x => x.Key, x => x.Value);

                sb.AppendLine($"{library.Id} {library.NumOfBooks}");
                sb.AppendLine(string.Join(" ", library.Books.Values));
            }

            return sb.ToString();
        }

        private static string Test2()
        {
            var sb = new StringBuilder();

            var booksReaded = new List<long>();

            sb.AppendLine(libObjects.Count().ToString());

            var librariesOrdered = libObjects.OrderByDescending(x => (x.NumOfBooks * x.BooksPerDay) - x.SingUpProcesDays);

            var daysSpent = 0;

            foreach (var library in librariesOrdered)
            {
                library.Books = library.Books.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                var booksCanProcess = (daysForScanning - (daysSpent - library.SingUpProcesDays) * library.BooksPerDay) - library.Books.Count();

                var books = library.Books.Take((int)booksCanProcess);

                sb.AppendLine($"{library.Id} {books.Count()}");
                sb.AppendLine(string.Join(" ", books.Select(x => x.Value)));

                daysSpent += library.SingUpProcesDays;
            }

            return sb.ToString();
        }

        private static string Test3()
        {
            var sb = new StringBuilder();

            var booksReaded = new List<long>();

            sb.AppendLine(libObjects.Count().ToString());

            var librariesOrdered = libObjects.OrderByDescending(x => (x.NumOfBooks * x.BooksPerDay) - x.SingUpProcesDays);

            var booksProcessed = new List<long>();

            var daysSpent = 0;

            foreach (var library in librariesOrdered)
            {
                //foreach (var processedBook in booksProcessed)
                //{
                //    library.Books.Remove(processedBook);
                //}

                library.Books = library.Books.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                var booksCanProcess = ((daysForScanning - (daysSpent + library.SingUpProcesDays)) * library.BooksPerDay);

                var books = library.Books.Take((int)booksCanProcess);

                //booksProcessed.AddRange(books.Select(x => x.Key));
                if (!books.Any()) break;
                 
                sb.AppendLine($"{library.Id} {books.Count()}");
                sb.AppendLine(string.Join(" ", books.Select(x => x.Value)));

                daysSpent += library.SingUpProcesDays;
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private static void ReadFileAndSetProps(string path)
        {
            string line;
            StreamReader file = new StreamReader(path);
            line = file.ReadLine();
            SetVarsLine1(line);

            line = file.ReadLine();
            SetVarsScoreBooks(line);

            long libraryId = default;
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;
                var libObj = new Library(libraryId);
                SetVarOfLibrary(line, libObj);
                line = file.ReadLine();
                SetVarsOfBooks(line, libObj);
                libObjects.Add(libObj);

                libraryId++;
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
                    scoreOfBooks.Add(qtt, val);
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
                    libObj.Books.Add(qtt, val);
                    currentVal = "";
                    qtt++;
                }
            }
            val = long.Parse(currentVal);
            libObj.Books.Add(qtt, val);
        }

        private static void SetVarOfLibrary(string line, Library libObj)
        {
            var values = line.Split(" ");

            libObj.NumOfBooks = long.Parse(values[0]);
            libObj.SingUpProcesDays = int.Parse(values[1]);
            libObj.BooksPerDay = long.Parse(values[2]);
        }

        private static void SetVarsLine1(string line)
        {
            var values = line.Split(" ");

            books = long.Parse(values[0]);
            libraries = long.Parse(values[1]);
            daysForScanning = long.Parse(values[2]) - 1;
        }
    }
}

