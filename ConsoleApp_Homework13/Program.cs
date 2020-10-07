using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Homework13
{
    class Program
    {
        static void Main(string[] args)
        {
            Person tom = new Person { Name = "Igor" };
            Console.WriteLine(SerializeToJson(tom));

            House car = new House() { Street = "Saltovka", Number = 12 };
            Console.WriteLine(SerializeToJson(car));

            Console.ReadKey();
        }

        static string SerializeToJson(object o)
        {
            StringBuilder result = new StringBuilder();
            var fields = o.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(x => x.GetCustomAttribute<MyIgnoreAttribute>() == null);
            if (!fields.Any())
            {
                throw new MyException();
            }
            result.Append("{");
            foreach (var f in fields)
            {
                result.Append("\"" + f.Name + "\":");
                if (f.GetValue(o).GetType() == "".GetType())
                {
                    result.Append("\"" + f.GetValue(o) + "\",");
                }
                else
                {
                    result.Append(f.GetValue(o) + ",");
                }
            }
            result[result.Length - 1] = '}';

            return result.ToString();
        }
    }

    class Person
    {
        public string Name;
        private int Age = 12;
    }

    class House
    {
        [MyIgnore]
        public string Street;
        [MyIgnore]
        public int Number;
    }
}
