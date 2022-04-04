using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEF
{
    class BookLoan
    {
        public int BookLoanId { get; set; }
        public BookCopy BookCopy { get; set; }
        public Borrower Borrower { get; set; }
        [AllowNull]
        public DateTime? LoanDate { get; set; }
        [AllowNull]
        public DateTime? ReturnDate { get; set; }
    }
}
