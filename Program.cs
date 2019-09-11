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
            List<String> list = new List<string>();
            list.Add("Horatio");
            list.Add("Harper");
            list.Add("Saori");

            // The first part is called the from clause and describes the source
            // child between the from and the in is called the range variable
            var children = from child in list
                           let fullName = child
                           where (fullName != "Saori")
                           select child.Backwards();

            // LINQ queries defer work as much as possible. No work is done until we start to enumerate with a foreach loop.
            foreach (string child in children)
            {
                Console.WriteLine("child: {0} has length: {1}", child, child.Length);
            }

            List<Object> objs = new List<Object>();
            objs.Add("Horatio");
            objs.Add(new Int64());
            objs.Add(new DateTime());
            objs.Add("Harper");

            // The filter operator Oftype<T> is useful when you have a collection that may contain a mixture of types,
            // and you only want to look at elements of a specific type.
            var objTypes = from objType in objs.OfType<String>()
                           orderby objType descending // Can contain an orderBy clause indicating the order in which you would like to items to emerge from query.
                           select objType;

            Console.WriteLine("\nSorting in descending order:");
            foreach (var objType in objTypes)
            {
                Console.WriteLine(objType);
            }

            Console.WriteLine("\nSorting in ascending order:");
            var sortedasc = objTypes.OrderBy(a => a);
            foreach (var sorted in sortedasc)
            {
                Console.WriteLine(sorted);
            }

            Console.WriteLine("\nSorting in descending order again:");
            var sortedesc = sortedasc.OrderByDescending(a => a);
            foreach (var sorted in sortedesc)
            {
                Console.WriteLine(sorted);
            }

            // Lazily combines two sequences into one.
            // This is an operator and there is no eqivalent in the query expression syntax.
            Console.WriteLine("\nConcatenated:");
            var concatenated = sortedasc.Concat(sortedesc);
            // Defer concatenation until you iterate through the sequence
            foreach (var con in concatenated)
            {
                Console.WriteLine(con);
            }

            List<Apple> listOfApples = new List<Apple>();
            listOfApples.Add(new Apple
            {
                Colour = "green",
                AppleType = "granny smith"
            });

            listOfApples.Add(new Apple
            {
                Colour = "green",
                AppleType = "granny smith"
            });

            listOfApples.Add(new Apple
            {
                Colour = "red",
                AppleType = "cocks"
            });

            var appleGrouping = from app in listOfApples
                                group app by app.Colour;

            foreach(var colouredApples in appleGrouping)
            {
                Console.WriteLine("\n" + colouredApples.Key + " coloured apples: ");
                foreach(var appleType in colouredApples)
                {
                    Console.WriteLine(appleType.AppleType);
                }
            }

            Console.WriteLine("\nProjections: apple colours: ");
            var colours = listOfApples.Select(a => a.Colour);
            foreach (var colour in colours)
            {
                Console.WriteLine(colour);
            }


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
