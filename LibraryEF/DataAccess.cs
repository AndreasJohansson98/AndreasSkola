using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace LibraryEF
{
    class DataAccess
    {
        public Context context { get; set; } = new Context();

        public List<BookCopy> AllAvailableBookCopies { get; set; } = new List<BookCopy>();

        public void RecreateDatabase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        public void CreateAuthor()
        {
            Console.Clear();
            Person p = new Person();
            Console.Write("Firstname: ");
            p.FirstName = Console.ReadLine();
            Console.Write("Lastname: ");
            p.LastName = Console.ReadLine();
            Author a = new Author();
            a.Person = p;
            a.Books = new List<Book>();
            context.Persons.Add(p);
            context.Authors.Add(a);
            context.SaveChanges();
        }
        public void RemoveAuthor()
        {
            var au = GetAuthors();
            PrintallAuthors(au);
            if(au.Count < 1)
            {
                Console.WriteLine("No Authors Found!");
                Console.ReadKey();
                return;
            }
            int selection = Helper.GetInt("Select an author to remove: ", au.Count) - 1;
            Author a = au.ElementAt(selection);
            au.Remove(a);
            context.Authors.Remove(a);
            context.SaveChanges();
        }
        public void RemoveBorrower()
        {
            var bo = GetBorrowers();
            if (bo.Count < 1)
            {
                Console.WriteLine("No Borrowers Found!");
                Console.ReadKey();
                return;
            }
            PrintallBorrowers(bo);
            int selection = Helper.GetInt("Select a borrower to remove: ", bo.Count) - 1;
            Borrower b = bo.ElementAt(selection);
            context.Borrowers.Remove(b);
            context.SaveChanges();
        }
        public void RemoveBook()
        {
            var bo = GetBooks();
            if (bo.Count < 1)
            {
                Console.WriteLine("No Books Found!");
                Console.ReadKey();
                return;
            }
            PrintallBooks(bo);
            int selection = Helper.GetInt("Select a book to remove: ", bo.Count) - 1;
            Book b = bo.ElementAt(selection);
            context.Books.Remove(b);
            context.SaveChanges();
        }
        public void CreateBorrower()
        {
            Console.Clear();
            Person p = new Person();
            Console.Write("Firstname: ");
            p.FirstName = Console.ReadLine();
            Console.Write("Lastname: ");
            p.LastName = Console.ReadLine();
            Borrower b = new Borrower();
            b.Person = p;
            Random rnd = new Random();
            b.LibraryCard = rnd.Next(1000, 100000);
            context.Persons.Add(p);
            context.Borrowers.Add(b);
            context.SaveChanges();
        }
        public void CreateBook()
        {
            Console.Clear();
            Book b = new Book();
            Console.Write("Title: ");
            b.Title = Console.ReadLine();
            Random rnd = new Random();
            b.Isbn = rnd.Next(1000, 100000);
            b.Grade = Helper.GetInt("Select grade(1-10): ",10);
            Console.Write("PublicationDate (yyyy/mm/dd): ");
            b.PublicationDate = Helper.GetDate();
            b.Authors = new List<Author>();
            bool run = true;
            while (run)
            {
               var authors = GetAuthors();
                if (authors.Count < 1)
                {
                    Console.WriteLine("No Authors Found!, Create authors before creating books!");
                    Console.ReadKey();
                    return;
                }
                PrintallAuthors(authors);
               int selectAuthor = Helper.GetInt("Select which author: ", authors.Count) - 1;
               Author a = authors.ElementAt(selectAuthor);
               b.Authors.Add(a);
                Console.WriteLine("Add more authors?");
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No");
                int selection = Helper.GetInt("Select: ", 2);
                if(selection == 2)
                {
                    run = false;
                }        
            }
            AddBookCopies(b);
            context.Books.Add(b);
            context.SaveChanges();
        }
        public BookCopy GetBookCopy()
        {
            var value = PrintAvailableBookCopies();
            if (value.Item1 > 0)
            {
                int selectBookCopy = Helper.GetInt("Select a bookcopy: ", value.Item1) - 1;
                return value.Item2.ElementAt(selectBookCopy);
            }
            else
            {
                return null;
            }
        }
        public void BorrowBook()
        {
            Console.Clear();
            var bo = GetBorrowers();
            if (bo.Count < 1)
            {
                Console.WriteLine("No Borrowers Found!");
                Console.ReadKey();
                return;
            }
            PrintallBorrowers(bo);
            int selectBorrower = Helper.GetInt("Select which borrower: ", bo.Count) - 1;
            Borrower b = bo.ElementAt(selectBorrower);
            BookLoan bl = new BookLoan();
            bl.Borrower = b;
            bl.BookCopy = GetBookCopy();
            if(bl.BookCopy == null)
            {
                return;
            }
            bl.LoanDate = DateTime.Now;
            bl.ReturnDate = null;
            bl.BookCopy.Borrowed = true;
            context.BookLoans.Add(bl);
            context.SaveChanges();
        }
        public void AddBookCopies(Book b)
        {
            int bookCopies = Helper.GetInt("How many copies: ", 1000000000);
            for (int i = 0; i < bookCopies; i++)
            {
                BookCopy bookCopy = new BookCopy();
                bookCopy.Book = b;
                AllAvailableBookCopies.Add(bookCopy);
                context.BookCopies.Add(bookCopy);
            }
        }
        public Tuple<int, List<BookCopy>> PrintAvailableBookCopies()
        {
            List<BookCopy> AvailableBookCopies = new List<BookCopy>();
            var BookCopies = GetBookCopies();
            int index = 1;
            foreach (BookCopy b in BookCopies)
            {
                if(b.Borrowed == false)
                {
                    Console.WriteLine("[" + index + "] Title:" + b.Book.Title + " Grade: " + b.Book.Grade + " Published: " + b.Book.PublicationDate);
                    AvailableBookCopies.Add(b);
                    index++;
                }    
            }
            return Tuple.Create (index - 1, AvailableBookCopies);
        }
        public void PrintallAuthors(List<Author> authors)
        {
            int index = 1;
            foreach (Author a in authors)
            {
                Console.WriteLine("[" + index + "]" + a.Person.FirstName + " " + a.Person.LastName);
                index++;
            }
        }
        public void PrintallBorrowers(List<Borrower> bo)
        {
            int index = 1;
            foreach (Borrower b in bo)
            {
                Console.WriteLine("[" + index + "]" + b.Person.FirstName + " " + b.Person.LastName);
                index++;
            }
        }
        public void PrintallBooks(List<Book> books)
        {
            int index = 1;
            foreach (Book b in books)
            {
                Console.WriteLine("[" + index + "] Title: " + b.Title + " Grade: " + b.Grade + " Published: " + b.PublicationDate);
                index++;
            }
        }
        public void FillDataBase()
        {
            //Author
            Person p1 = new Person();
            p1.FirstName = "Kalle";
            p1.LastName = "Kula";
            //Borrower
            Person p2 = new Person();
            p2.FirstName = "Pelle";
            p2.LastName = "P";
            //Author + Borrower
            Person p3 = new Person();
            p3.FirstName = "Andreas";
            p3.LastName = "J";

            //Kalle
            Author a1 = new Author();
            a1.Person = p1;
            a1.Books = new List<Book>();

            //Andreas
            Author a2 = new Author();
            a2.Person = p3;
            a2.Books = new List<Book>();
            Borrower l2 = new Borrower();
            l2.LibraryCard = 4321;
            l2.Person = p3;
            //Pelle
            Borrower l1 = new Borrower();
            l1.LibraryCard = 1234;
            l1.Person = p2;

            // Bibeln
            Book b1 = new Book();
            b1.Title = "Bibeln";
            b1.Isbn = 666;
            b1.Grade = 1;
            b1.Authors = new List<Author>();
            b1.Authors.Add(a1);
            a1.Books.Add(b1);

            // 3 ex of Bibeln
            BookCopy bc1 = new BookCopy();
            bc1.Book = b1;
            BookCopy bc2 = new BookCopy();
            bc2.Book = b1;
            BookCopy bc3 = new BookCopy();
            bc3.Book = b1;
            AllAvailableBookCopies.Add(bc1);
            AllAvailableBookCopies.Add(bc2);
            AllAvailableBookCopies.Add(bc3);
            //BookLoan bl1 = BorrowBook(bc1, l2); // Andreas lånar bibeln
            //BookLoan bl2 = BorrowBook(bc3, l1); // Pelle lånar bibeln
            //ReturnBook(bl1); // Andreas lämnar tillbaka bibeln

            context.Persons.Add(p1);
            context.Persons.Add(p2);
            context.Persons.Add(p3);
            context.Authors.Add(a1);
            context.Authors.Add(a2);
            context.Borrowers.Add(l1);
            context.Borrowers.Add(l2);
            context.Books.Add(b1);
            context.BookCopies.Add(bc1);
            context.BookCopies.Add(bc2);
            context.BookCopies.Add(bc3);
            context.SaveChanges();
        }
        public BookLoan BorrowBook(BookCopy bc, Borrower b)
        {
            BookLoan bl = new BookLoan();
            bl.BookCopy = bc;
            bl.Borrower = b;
            bl.LoanDate = DateTime.Now;
            bl.ReturnDate = null;
            bc.Borrowed = true;
            b.BookLoans.Add(bl);
            context.BookLoans.Add(bl);
            return bl;
        }
        public void ReturnBook()
        {
            var bo = GetBorrowers();
            PrintallBorrowers(bo);
            int selectBorrower = Helper.GetInt("Select which borrower: ", bo.Count) - 1;
            Borrower b = bo.ElementAt(selectBorrower);
            if(b.BookLoans.Count > 0)
            {
                var value = PrintBookLoans(b);
                int selectBookLoan = Helper.GetInt("Select bookloan to return: ", value.Item1) - 1;
                BookLoan bl = value.Item2.ElementAt(selectBookLoan);
                bl.ReturnDate = DateTime.Now;
                bl.BookCopy.Borrowed = false;
            }
            context.SaveChanges();
        }
        public Tuple<int, List<BookLoan>> PrintBookLoans(Borrower b)
        {

            List<BookLoan> AvailableBookLoans = new List<BookLoan>();
            int index = 1;
            foreach (BookLoan bl in b.BookLoans)
            {
                if (bl.ReturnDate == null)
                {
                    Console.WriteLine("[" + index + "] Title: " + bl.BookCopy.Book.Title + " LoanDate :" + bl.LoanDate);
                    index++;
                    AvailableBookLoans.Add(bl);
                }
            }
            return Tuple.Create(index - 1, AvailableBookLoans);
        }
        public void InspectLibrary(List<Borrower> borrowers, List<BookLoan> bookLoans)
        {
            Console.Clear();
            var authors = GetAuthors();
            foreach (var a in authors)
            {
                Console.WriteLine("Author: " + a.Person.FirstName + " " + a.Person.LastName);
                foreach (var book in a.Books)
                {
                    Console.WriteLine("       Title: " + book.Title);
                }
            }
            foreach (var b in borrowers)
            {
                Console.WriteLine("Borrower: " + b.Person.FirstName + " " + b.Person.LastName);
                foreach (BookLoan bl in b.BookLoans)
                {
                    Console.WriteLine("     Title: " + bl.BookCopy.Book.Title + " LoanDate: " + bl.LoanDate + " ReturnDate: " + bl.ReturnDate);
                }
            }
            var bcs = GetBookCopies();
            foreach (var bc in bcs)
            {
                Console.WriteLine("BookCopy Title: " + bc.Book.Title + " Borrowed: " + bc.Borrowed);
            }
            Console.ReadKey();
        }
        public List<Author> GetAuthors()
        {
            return context.Authors.Include(a => a.Person).ToList();
        }
        public List<Borrower> GetBorrowers()
        {
            return context.Borrowers.Include(b => b.Person).ToList();
        }
        public List<BookLoan> GetBookLoans()
        {
            return context.BookLoans.Include(b => b.Borrower).ToList();
        }
        public List<BookCopy> GetBookCopies()
        {
            return context.BookCopies.Include(b => b.Book).ToList();
        }
        public List<Book> GetBooks()
        {
            return context.Books.Include(b => b.BookCopies).ToList();
        }
    }
}
