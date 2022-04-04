using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem
{
    public class Customer : User
    {
        public override string UserName { get; set; }
        public override string Password { get; set; }

        public override bool IsAdmin { get; set; }

        public Customer(string userName, string password) : base(userName, password, false)
        {

        }
        public void PrintUserMenu()
        {
            Console.Clear();
            PrintBookings();
            Console.WriteLine("[1] Create booking");
            Console.WriteLine("[2] Remove booking");
            Console.WriteLine("[3] Show previous bookings");
            Console.WriteLine("[4] Log out");
        }

        public override void UserMenu()
        {
            bool run = true;
            while (run)
            {
                PrintUserMenu();
                int menuSelection = Helper.GetInt("Select between 1-4: ", 4);
                switch (menuSelection)
                {
                    case 1:
                        var booking = Booking.BookGiraffe();
                        if (booking == null)
                        {
                            break;
                        }
                        else
                        {
                            BookedGiraffes.Add(booking);
                        }
                        break;

                    case 2:
                        RemoveBooking();
                        break;

                    case 3:
                        PrintBookingHistory();
                        break;

                    case 4:
                        run = false;
                        break;
                }
            }
        }

        public void RemoveBooking()
        {
            PrintBookingsWithIndex();
            if (BookedGiraffes.Count == 0)
            {
                Console.WriteLine("\nPress any key to go back!");
                Console.ReadKey();
            }
            else
            {
                int removeSelection = Helper.GetInt("Choose which booking to remove: ", BookedGiraffes.Count) - 1;
                BookedGiraffes[removeSelection].Giraffe.IsAvailable = true;
                BookingHistoryLog(removeSelection);
                BookedGiraffes.RemoveAt(removeSelection);
            }

        }

        public void BookingHistoryLog(int removeSelection)
        {
            BookingHistoryEntry bookingHistoryEntry = new BookingHistoryEntry(BookedGiraffes[removeSelection].Giraffe.ToString());

            if (!BookingHistory.Contains(bookingHistoryEntry))
            {
                BookingHistory.Add(bookingHistoryEntry);
            }
        }

        public void PrintBookingHistory()
        {
            Console.WriteLine("You have previously booked these giraffes (note: giraffes booked multiple times on show up once in history log!) ");
            foreach (var giraffeInfo in BookingHistory)
            {
                Console.WriteLine(giraffeInfo.GiraffeInfo);
            }
            Console.WriteLine("\nPress any key to go back!");
            Console.ReadKey();
        }

        public void PrintBookings()
        {
            if (BookedGiraffes.Count > 0)
            {
                Console.WriteLine("Your current booked Giraffes: ");
                foreach (var booking in BookedGiraffes)
                {
                    Console.WriteLine(booking.Giraffe.ToString());
                }
            }
            else
            {
                Console.WriteLine("Currently no giraffes booked!");
            }
        }

        public void PrintBookingsWithIndex()
        {
            Console.Clear();
            Console.WriteLine("Your current booked Giraffes: ");
            int index = 1;
            if (BookedGiraffes.Count > 0)
            {
                foreach (var booking in BookedGiraffes)
                {
                    Console.WriteLine("[" + index + "]" + booking.Giraffe.ToString());
                    index++;
                }
            }
            else
            {
                Console.WriteLine("Currently no giraffes booked");
            }
        }
    }
}
