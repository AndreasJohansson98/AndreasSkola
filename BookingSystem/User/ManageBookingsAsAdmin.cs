using System;
using System.Linq;

namespace BookingSystem
{

    public class ManageBookingsAsAdmin
    {
        private static BookingSystem BookingSystem { get; set; }
        public static void Init(BookingSystem bookingSystem)
        {
            BookingSystem = bookingSystem;
        }
        public static void ManageBookingsMenu()
        {
            bool run = true;
            while (run)
            {
                PrintManageBookingMenu();
                int menuSelection = Helper.GetInt("Select between 1-3: ", 3);
                switch (menuSelection)
                {
                    case 1:
                        User customer = SelectCustomer();
                        if (customer != null)
                        {
                            var newBooking = Booking.BookGiraffe();
                            if (newBooking != null)
                            {
                                customer.BookedGiraffes.Add(newBooking);
                            }
                        }
                        break;
                    case 2:
                        Customer customer2 = SelectCustomer();
                        if (customer2 != null)
                        {
                            customer2.RemoveBooking();
                        }
                        break;
                    case 3:
                        run = false;
                        break;
                }
            }
        }

        public static void PrintManageBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] Create Booking for a customer");
            Console.WriteLine("[2] Bring back a giraffe from a customer");
            Console.WriteLine("[3] Go back to previous menu");
        }

        public static void PrintAllCustomers()
        {
            int index = 1;
            foreach (var user in BookingSystem.AllUsers)
            {
                if (!user.Value.IsAdmin)
                {
                    Console.WriteLine("[" + index + "]" + user.Key + " - Customer");
                    index++;
                }
            }
        }

        public static Customer SelectCustomer()
        {
            PrintAllCustomers();
            var allCustomers = BookingSystem.AllUsers.Where(customer => customer.Value.IsAdmin is false).ToList();
            int selection = Helper.GetInt("Select which customers bookings you want to manage: ", allCustomers.Count) - 1;

            string key = allCustomers.ElementAt(selection).Key;
            if (!BookingSystem.AllUsers.TryGetValue(key, out User customer))
            {
                return null;
            }
            return (customer as Customer);
        }
    }
}