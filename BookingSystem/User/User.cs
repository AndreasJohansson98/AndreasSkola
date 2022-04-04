using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace BookingSystem
{
    public abstract class User 
    {
        public  abstract string UserName { get; set; }
        public  abstract string Password { get; set; }
        public abstract  bool IsAdmin { get; set; }

        public List<BookingHistoryEntry> BookingHistory { get; set; } = new List<BookingHistoryEntry>();
        public List<Booking> BookedGiraffes { get; set; } = new List<Booking>();

        public string ToJSON() => JsonConvert.SerializeObject(this);
        static public User FromJSON(string json) => JsonConvert.DeserializeObject<User>(json);

        public User(string userName, string password, bool isAdmin)
        {
            UserName = userName;
            Password = password;
            IsAdmin = isAdmin;
        }
        public abstract void UserMenu();
        
    }
}
