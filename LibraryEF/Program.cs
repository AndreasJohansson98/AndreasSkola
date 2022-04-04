using Microsoft.Data.SqlClient;
using System;

namespace LibraryEF
{
    internal class Program
    {
        private static DataAccess dataAccess = new DataAccess();

        private static void Main(string[] args)
        {
            dataAccess.RecreateDatabase();
            dataAccess.FillDataBase();
            Menu();
            Console.ReadKey();
        }

        public static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] Create Author");
            Console.WriteLine("[2] Create Borrower");
            Console.WriteLine("[3] Create Book");
            Console.WriteLine("[4] Borrow Book");
            Console.WriteLine("[5] Remove Author");
            Console.WriteLine("[6] Remove Borrower");
            Console.WriteLine("[7] Remove Book");
            Console.WriteLine("[8] Return Book");
            //Console.WriteLine("[9] Inspect Library");
            Console.WriteLine("[9] Quit");
        }

        public static void Menu()
        {
            bool run = true;
            while (run)
            {
                PrintMenu();
                int selection = Helper.GetInt("Select: ", 9);
                switch (selection)
                {
                    case 1:
                        dataAccess.CreateAuthor();
                        break;

                    case 2:
                        dataAccess.CreateBorrower();
                        break;

                    case 3:
                        dataAccess.CreateBook();
                        break;

                    case 4:
                        dataAccess.BorrowBook();
                        break;

                    case 5:
                        dataAccess.RemoveAuthor();
                        break;

                    case 6:
                        dataAccess.RemoveBorrower();
                        break;

                    case 7:
                        dataAccess.RemoveBook();
                        break;

                    case 8:
                        dataAccess.ReturnBook();
                        break;

                    //case 9:
                    //    var b = dataAccess.GetBorrowers();
                    //    var bl = dataAccess.GetBookLoans();
                    //    dataAccess.InspectLibrary(b, bl);
                    //    break;

                    case 9:
                        run = false;
                        break;
                }
            }
        }
    }
}