using System;


namespace BookingSystem
{
    public class Helper
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
        public static double GetDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                if (Double.TryParse(Console.ReadLine(), out double value))
                {
                    if (value >= 1)
                    {
                        return value;
                    }
                }
                Console.WriteLine("Wrong input, please try again");
            }
        }
    }
}