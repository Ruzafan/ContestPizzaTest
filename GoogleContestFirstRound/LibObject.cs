using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleContestFirstRound
{
    public class Library
    {
        public long Id { get; set; }
        public long NumOfBooks { get; set; }

        public long SingUpProcesDays { get; set; }

        public long BooksPerDay { get; set; }

        public Dictionary<long,long> Books { get; set; }
        public bool IsSigningUp { get; set; }
        public bool IsSignedUp { get; set; }

        //public List<int> DaysSingingIn { get; set; }

        public Library(long id)
        {
            Id = id;
            Books = new Dictionary<long, long>();
        }
    }
}
