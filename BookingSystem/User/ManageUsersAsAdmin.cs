using System;
using System.Linq;
using BookingSystem;

namespace BookingSystem
{
    public class ManageUsersAsAdmin
    {
        private static BookingSystem BookingSystem { get; set; }
        public static void Init(BookingSystem bookingSystem)
        {
            BookingSystem = bookingSystem;
        }
        public static void PrintManageUsersMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] Create admin");
            Console.WriteLine("[2] Create customer");
            Console.WriteLine("[3] Show all existing users");
            Console.WriteLine("[4] Remove user");
            Console.WriteLine("[5] Go back to previous menu");
        }

        public static void ManageUsersMenu()
        {
            bool run = true;
            while (run)
            {
                PrintManageUsersMenu();
                int menuSelection = Helper.GetInt("Select between 1-5: ", 5);
                switch (menuSelection)
                {
                    case 1:
                        CreateAdmin();
                        break;

                    case 2:
                        CreateCustomer();
                        break;
                    case 3:
                        PrintAllUsers();
                        break;
                    case 4:
                        RemoveUser();
                        if (BookingSystem.AllUsers.Count == 0)
                        {
                            AllUsersGone();
                        }
                        break;

                    case 5:
                        run = false;
                        break;
                }
            }
        }

        public static void CreateCustomer()
        {
            Console.Clear();
            Console.Write("Type in a username: ");
            string userName = Console.ReadLine();
            Console.Write("Type in a password: ");
            string password = Console.ReadLine();
            Customer newCustomer = new Customer(userName, password);
            try
            {
                BookingSystem.AllUsers.Add(userName, newCustomer);
            }
            catch (Exception)
            {
                Console.WriteLine("That username already exists!");
                Console.ReadKey();
            }
        }

        public static void CreateAdmin()
        {
            Console.Clear();
            Console.Write("Type in a username: ");
            string userName = Console.ReadLine();
            Console.Write("Type in a password: ");
            string password = Console.ReadLine();
            User newAdmin = new Admin(userName, password);
            BookingSystem.AllUsers.Add(userName, newAdmin);
        }

        public static void RemoveUser()
        {
            PrintAllUsersWithIndex();
            int removeSelection = Helper.GetInt("Select what number you want to remove: ", BookingSystem.AllUsers.Count) - 1;

            BookingSystem.AllUsers.Remove(BookingSystem.AllUsers.ElementAt(removeSelection).Key);
        }

        public static void AllUsersGone()
        {
            Console.Clear();
            Console.WriteLine("YOU HAVE REMOVED ALL USERS ??????");
            Console.WriteLine("YOU CHEEKY BASTARD!!!!!");
            Console.WriteLine("\nPress any key to quit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static void PrintAllUsersWithIndex()
        {
            int index = 1;
            foreach (var user in BookingSystem.AllUsers)
            {
                if (user.Value.IsAdmin)
                {
                    Console.WriteLine("[" + index + "]" + user.Key + " - Admin");
                }
                else
                {
                    Console.WriteLine("[" + index + "]" + user.Key + " - Customer");
                }
                index++;
            }
        }
        public static void PrintAllUsers()
        {
            Console.Clear();
            Console.WriteLine("List of all the users:");
            var allUsersSorted = BookingSystem.AllUsers.OrderBy(user => user.Value.IsAdmin).ThenBy(user => user.Key).ToList();
            foreach (var user in allUsersSorted)
            {
                if (user.Value.IsAdmin)
                {
                    Console.WriteLine(user.Key + " - Admin");
                }
                else
                {
                    Console.WriteLine(user.Key + " - Customer");
                }
            }
            Console.WriteLine("\nPress any key to go back!");
            Console.ReadKey();
        }
    }
}