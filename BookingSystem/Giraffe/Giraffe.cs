using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookingSystem
{
    public class Giraffe
    {
        public string Name { get; set; }

        public Sex Sex { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public bool IsAvailable { get; set; }
        public string ToJSON() => JsonConvert.SerializeObject(this);
        static public Giraffe FromJSON(string json) => JsonConvert.DeserializeObject<Giraffe>(json);

        public Giraffe(string name, Sex sex, int age, double height, bool isAvailable)
        {
            Name = name;
            Sex = sex;
            Age = age;
            Height = height;
            IsAvailable = isAvailable;
        }
        public override string ToString()
        {
            return "Name: " + Name + " Age: " + Age + " Height: " + Height;
        }
        public bool Equals(Giraffe other)
        {
            if (other == null) return false;
            return this.Name == other.Name;
        }
    }
}
