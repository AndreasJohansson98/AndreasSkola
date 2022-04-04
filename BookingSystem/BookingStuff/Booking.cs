using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem
{
    public class Booking
    {
        private static BookingSystem BookingSystem { get; set; }
        public static void Init(BookingSystem bookingSystem)
        {
            BookingSystem = bookingSystem;
        }
        public Giraffe Giraffe { get; set; }

        public static Booking BookGiraffe()
        {
            List<Giraffe> availableGiraffes = BookingSystem.AllGiraffes.Where(giraffe => giraffe.IsAvailable).ToList();
            if (availableGiraffes.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No giraffes are currently available to book :(");
                Console.WriteLine("\n" +"Press any button to continue");
                Console.ReadKey();
                return null;
            }
            Booking booking = new Booking();
            PrintAllAvailableGiraffes();
            int selectGiraffe = Helper.GetInt("Select which Giraffe to book: ", availableGiraffes.Count) - 1;
            booking.Giraffe = availableGiraffes.ElementAt(selectGiraffe);
            availableGiraffes.ElementAt(selectGiraffe).IsAvailable = false;

            return booking;
            
        }

        public static void PrintAllAvailableGiraffes()
        {
            Console.Clear();
            List<Giraffe> availableGiraffes = BookingSystem.AllGiraffes.Where(giraffe => giraffe.IsAvailable).ToList();
            int index = 1;
            Console.WriteLine("These are the giraffes available for booking!");
            foreach (Giraffe giraffe in availableGiraffes)
            {
                Console.WriteLine("[" + index + "]" + giraffe.ToString());
                index++;
            }
        }
    }
}