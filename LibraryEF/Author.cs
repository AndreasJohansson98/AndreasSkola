using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEF
{
    class Author
    {
        public int AuthorId { get; set; }
        public Person Person { get; set; }
        [AllowNull]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
