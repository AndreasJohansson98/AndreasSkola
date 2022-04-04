using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem
{
    public class Admin : User
    {
        public override string UserName { get; set; }
        public override string Password { get; set; }

        public override bool IsAdmin { get; set; }

        public Admin(string userName, string password) : base(userName,password,true)
        {

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
                        ManageUsersAsAdmin.ManageUsersMenu();
                        break;
                    case 2:
                        ManageGiraffesAsAdmin.ManageGiraffesMenu();
                        break;
                    case 3:
                        ManageBookingsAsAdmin.ManageBookingsMenu();
                        break;
                    case 4:
                        run = false;
                        break;
                }
            }
        }
        public void PrintUserMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] Manage Users");
            Console.WriteLine("[2] Manage Giraffes");
            Console.WriteLine("[3] Manage Bookings");
            Console.WriteLine("[4] Log out");
        }
    }
}
