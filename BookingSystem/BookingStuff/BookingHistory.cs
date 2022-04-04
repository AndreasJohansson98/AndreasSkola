using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem
{
    public class BookingHistoryEntry : IEquatable<BookingHistoryEntry>
    {
        public string GiraffeInfo { get; set; }


        public BookingHistoryEntry(string giraffeInfo)
        {
            GiraffeInfo = giraffeInfo;
        }
        public bool Equals(BookingHistoryEntry other)
        {
            if (other == null) return false;
            return this.GiraffeInfo == other.GiraffeInfo;
        }

    }
}
