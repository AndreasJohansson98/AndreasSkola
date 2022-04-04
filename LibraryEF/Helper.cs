using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEF
{
    class Helper
    {
        public static int GetInt(string prompt, int maxValue)
        {
            while (true)
            {
                Console.Write(prompt);

                if (Int32.TryParse(Console.ReadLine(), out int value))

                {
                    if (value <= maxValue && value >= 1)
                    {
                        return value;
                    }
                }
                Console.WriteLine("Wrong input, please try again");
            }
        }
        public static DateTime GetDate()
        {
            string line = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(line, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Invalid date, please retry");
                line = Console.ReadLine();
            }
            return dt;
        }
    }
}
