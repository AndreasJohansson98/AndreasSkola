using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingSystem
{
    public class ManageGiraffesAsAdmin
    {
        private static BookingSystem BookingSystem { get; set; }
        public static void Init(BookingSystem bookingSystem)
        {
            BookingSystem = bookingSystem;
        }
        public static void PrintManageGiraffeMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] Create Giraffe");
            Console.WriteLine("[2] Show all Giraffes");
            Console.WriteLine("[3] Remove Giraffe");
            Console.WriteLine("[4] Go back to previous menu");
        }

        public static void ManageGiraffesMenu()
        {
            bool run = true;
            while (run)
            {
                PrintManageGiraffeMenu();
                int menuSelection = Helper.GetInt("Select between 1-4: ", 4);
                switch (menuSelection)
                {
                    case 1:
                        CreateGiraffe();
                        break;
                    case 2:
                        PrintAllGiraffes();
                        break;
                    case 3:
                        RemoveGiraffe();
                        break;
                    case 4:
                        run = false;
                        break;
                }
            }
        }

        public static void CreateGiraffe()
        {
            Console.Clear();
            Console.Write("Type in a name: ");
            string name = Console.ReadLine();
            Sex sex = Sex.Male;
            int age = Helper.GetInt("Type in what age: ", int.MaxValue);
            double height = Helper.GetDouble("Type in how tall: ");
            Giraffe newGiraffe = new Giraffe(name, sex, age, height, true);
            BookingSystem.AllGiraffes.Add(newGiraffe);
        }

        public static void RemoveGiraffe()
        {
            List<Giraffe> availableGiraffes = BookingSystem.AllGiraffes.Where(giraffe => giraffe.IsAvailable).ToList();
            if (availableGiraffes.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("WHERE THE HELL DID ALL THE GIRAFFES HEAD OFF TO???");
                Console.WriteLine("\nPress any key to go back!");
                Console.ReadKey();
            }
            else
            {
                PrintAllAvailableGiraffes();
                int removeSelection = Helper.GetInt("Select what giraffe you want to remove: ", availableGiraffes.Count) - 1;
                BookingSystem.AllGiraffes.Remove(BookingSystem.AllGiraffes[removeSelection]);
            }
        }
        public static void PrintAllAvailableGiraffes()
        {
            Console.Clear();
            Console.WriteLine("List of all giraffes currently not booked:");
            int index = 1;
            List<Giraffe> availableGiraffes = BookingSystem.AllGiraffes.Where(giraffe => giraffe.IsAvailable).ToList();
            foreach (var giraffe in availableGiraffes)
            {
                Console.WriteLine("[" + index + "]" + giraffe.ToString());
                index++;
            }
        }
        public static void PrintAllGiraffes()
        {
            Console.Clear();
            Console.WriteLine("Down below is a list of all giraffes: ");
            var allGiraffesSorted = BookingSystem.AllGiraffes.OrderBy(giraffe => giraffe.IsAvailable).ThenBy(giraffe => giraffe.Name).ToList();
            foreach (var giraffe in allGiraffesSorted)
            {
                if (giraffe.IsAvailable)
                { 
                     Console.WriteLine(giraffe.ToString() + " - Available to book");
                }
                else
                {
                    Console.WriteLine(giraffe.ToString() + " - Currently booked");
                }
            }
            Console.WriteLine("\nPress any key to go back!");
            Console.ReadKey();
        }
    }
}