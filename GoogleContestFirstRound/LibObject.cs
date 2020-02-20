using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleContestFirstRound
{
    public class Library
    {
        public long NumOfBooks { get; set; }

        public long SingUpProcesDays { get; set; }

        public long BooksPerDay { get; set; }

        public Dictionary<long,long> Books { get; set; }

        public Library()
        {
            Books = new Dictionary<long, long>();
        }
    }
}
