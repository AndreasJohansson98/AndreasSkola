using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BookingSystem
{
    public class BookingSystem
    {
        public Dictionary<string, User> AllUsers { get; set; } = new Dictionary<string, User>();
        public List<Giraffe> AllGiraffes { get; set; } = new List<Giraffe>();

        public static string _DBFile = "BookingSystem.json";
        public static string _DBBootStrapFile = "BookingSystem.bootstrap.json";

        public string ToJSON() => JsonConvert.SerializeObject(this);

        static public BookingSystem FromJSON(string json) => JsonConvert.DeserializeObject<BookingSystem>(json, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
        public BookingSystem()
        {
            ManageBookingsAsAdmin.Init(this);
            ManageUsersAsAdmin.Init(this);
            ManageGiraffesAsAdmin.Init(this);
            Booking.Init(this);
        }
        public static BookingSystem LoadData()
        {
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(_DBFile))
                {
                    json = sr.ReadToEnd();
                    Console.WriteLine($"We read in: {json}");
                    return FixBookingReferences(FromJSON(json));
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            try
            {
                using (StreamReader sr = new StreamReader(_DBBootStrapFile))
                {
                    json = sr.ReadToEnd();
                    Console.WriteLine($"We read in: {json}");
                    return FixBookingReferences(FromJSON(json));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }
        public static BookingSystem FixBookingReferences(BookingSystem bookingSystem)
        {
            foreach (var user in bookingSystem.AllUsers)
            {
                foreach (var booking in user.Value.BookedGiraffes)
                {
                    var giraffe = bookingSystem.AllGiraffes.Find(g => g.Equals(booking.Giraffe));
                    booking.Giraffe = giraffe; //<--- Byter ut object mot en referens i AllGiraffe listan
                }
            }
            return bookingSystem;
        }
        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            try
            {
                using (StreamWriter sw = new StreamWriter(_DBFile))
                {
                    sw.Write(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] Log in");
            Console.WriteLine("[2] Quit");
        }

        public void StartMenu()
        {
            bool run = true;
            while (run)
            {
                PrintMenu();
                int menuSelection = Helper.GetInt("Select between 1-2: ", 2);
                switch (menuSelection)
                {
                    case 1:
                        User currentUser = LogIn();
                        if (currentUser == null)
                        {
                            break;
                        }
                        currentUser.UserMenu();
                        break;

                    case 2:
                        run = false;
                        break;
                }
            }
        }

        public User LogIn()
        {
            Console.Clear();
            Console.Write("Type in your username: ");
            string userName = Console.ReadLine();
            Console.Write("Type in your password: ");
            string password = Console.ReadLine();
            if (AllUsers.ContainsKey(userName) && AllUsers[userName].Password == password)
            {
                Console.Clear();
                Console.WriteLine("Login Succesful, Welcome " + userName + "!");
                Console.WriteLine("\nPress any button to continue");
                Console.ReadKey();
                return AllUsers[userName];
            }
            else
            {
                Console.WriteLine("Login failed, try again!");
                Console.WriteLine("\nPress any button to continue");
                Console.ReadKey();
                return null;
            }
        }
    }
}