using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEF
{
    class Borrower
    {
        public int BorrowerId { get; set; }
        public Person Person { get; set; }
        public int LibraryCard { get; set; }

        public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
    }
}
