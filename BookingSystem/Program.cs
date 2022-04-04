using System;


namespace BookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            //BootStrap();
            BookingSystem bookingSystem = BookingSystem.LoadData();
            Console.Clear();
            Console.WriteLine(
                "Admin login med UserName: Admin och det fyndiga Password: Password\n");
            Console.ReadKey();
            bookingSystem.StartMenu();
            bookingSystem.SaveData();
        }
        public static void BootStrap()
        {
            Admin originalAdmin = new Admin("Admin", "Password");
            BookingSystem bookingSystem = new BookingSystem();
            bookingSystem.AllUsers.Add(originalAdmin.UserName, originalAdmin);
            bookingSystem.SaveData();
        }
    }
}
