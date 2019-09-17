using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqExample.main();

            CollectionExample.main();

            DelegateExample.main();

            Console.WriteLine("All done");
        }

    }      

    // In order to support LINQ, a feature called exteension methods have been added.
    // These methods bolted onto a type by some other type.
    // You can add new methods to a type even if you can't change that type.
    // If the class is added to a namespace and that namespace is addedto a class then any string will have the extension method.
    // SELECT and WHERE are examples of LINQ operators and extension methods defined by the System.Linq namespace from the static class Enumerable
    // The Enumerable class defines these 2 methods and others for the IEnumerable<T> 
    static class StringAdditions // Must be a static class.
    {
        // Must be a static function.
        public static string Backwards(this string input) // Must specify this keyword as the first parameter.
        {
            char[] characters = input.ToCharArray();
            Array.Reverse(characters);
            return new string(characters);
        }
    }

    class Apple
    {
        public String Colour
        {
            get;
            set;
        }
        public String AppleType
        {
            get;
            set;
        }
    }
}
