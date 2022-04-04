using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEF
{
    class BookCopy
    {
        public int BookCopyId { get; set; }

        public bool Borrowed { get; set; } = false;
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
