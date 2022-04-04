using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEF
{
    class Book
    {
        public int BookId { get; set; }
        public int Isbn { get; set; }

        public string Title { get; set; }
        
        public int Grade { get; set; }

        public DateTime PublicationDate { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();

        public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
    }
}
