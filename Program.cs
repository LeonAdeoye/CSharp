﻿using System;
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
                           orderby objType descending // Here a query expression contains an orderBy clause indicating the order in which you would like to items to emerge from query.
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

            foreach (var colouredApples in appleGrouping)
            {
                Console.WriteLine("\n" + colouredApples.Key + " coloured apples: ");
                foreach (var appleType in colouredApples)
                {
                    Console.WriteLine(appleType.AppleType);
                }
            }

            // You can use 'projection' lambda expressions with the Select operator. 
            Console.WriteLine("\nProjections: apple colours: ");
            var colours = listOfApples.Select(a => a.Colour);
            foreach (var colour in colours)
            {
                Console.WriteLine(colour);
            }

            List<CalendarEvent> events = new List<CalendarEvent>
            {
                new CalendarEvent
                {
                    Title = "Programming at home",
                    StartTime = new DateTimeOffset(2019, 9, 13, 20, 43, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(4)

                },
                new CalendarEvent
                {
                    Title = "Cooking",
                    StartTime = new DateTimeOffset(2019, 9, 13, 22, 43, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(3)
                },
                new CalendarEvent
                {
                    Title = "Chess game",
                    StartTime = new DateTimeOffset(2019, 9, 12, 20, 43, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(1)
                },
                new CalendarEvent
                {
                    Title = "Swimming",
                    StartTime = new DateTimeOffset(2019, 9, 12, 22, 43, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(3)
                }

            };

            // This projection constructs a new anaonymous object for each item.
            // All we have is the object initialization syntax to populate the various properties.
            var projected = from ev in events
                            select new
                            {
                                Title = ev.Title,
                                StartTime = ev.StartTime,
                                EndTime = ev.StartTime + ev.Duration
                            };

            // Drop the first item and show the next 2.
            var taken = projected.Skip(1).Take(2);

            foreach (var item in taken)
            {
                Console.WriteLine("{0} event starts {1} and ends at {2}", item.Title, item.StartTime, item.EndTime);
            }

            DateTimeOffset newEventStart = new DateTimeOffset(2019, 9, 13, 20, 00, 20, 10, TimeSpan.Zero);
            TimeSpan newEventDuration = TimeSpan.FromHours(20);
            var overlapsAny = events.Any(ev => CalendarEvent.TimesOverlap(ev.StartTime, ev.Duration, newEventStart, newEventDuration));
            var overlapsAll = events.All(ev => CalendarEvent.TimesOverlap(ev.StartTime, ev.Duration, newEventStart, newEventDuration));
            Console.WriteLine("Any overlap exists: {0}, and All overlap exists: {1}", overlapsAny, overlapsAll);

            Console.WriteLine("Sum hours: {0}", events.Sum(ev => ev.Duration.TotalHours));
            Console.WriteLine("Max: {0}", events.Max(ev => ev.Duration.TotalHours));
            Console.WriteLine("Min: {0}", events.Min(ev => ev.Duration.TotalHours));

            // The first argument is the seed value that will be built up as the aggregation runs.
            // The second argument is the delegate that will be invoked once for each item. 
            // It will be passed the current aggregated value (initially the seed value) and the current item.
            // Whatever this delegate returns becomes the new aggregated value, that will be passed in as the first argument when that delegate is called for the next item.
            Console.WriteLine("Sum minutes: {0}", events.Aggregate(0.0, (total, ev) => total + ev.Duration.TotalMinutes));

            var dict = events.ToDictionary(ev => ev.Title, ev => ev.Duration);

            int number = 8;
            int[] values = new int[] { 1, 2, 3, 4 };
            var nums = values.ToDictionary(v => v, v => v);
            if (nums.TryGetValue(4, out number))
            {
                Console.WriteLine("Found 4, here is the value: {0}.", number);
            }

            try
            {
                nums.Add(6, 6);
                nums.Add(1, 6);
            }
            catch(Exception argumentException)
            {
                Console.WriteLine("Unlike the indexer, the dictionary 'Add' method will throw an exception {0} if you add an entry that already exists.", nameof(argumentException));
            }

            if (!nums.TryGetValue(5, out number))
            {
                // because false 'out number' will reset number to zero
                Console.WriteLine("Did not find 5, here is the value: {0}.", number);
            }

            if (nums.ContainsKey(3))
                Console.WriteLine("Dictionary key 3 found.");

            var familyDictionary = new Dictionary<string, string>
            {
                {"Horatio","Son of Leon"},
                {"Harper", "Daughter of Leon"}
            };

            familyDictionary["Saori"] = "Wife of Leon";

            foreach (var member in familyDictionary)
            {
                Console.WriteLine("{0}: {1}.", member.Key, member.Value);
            }

            // Use TryGetValue when you're not sure if the dictionary will contain the entry you're lookign for.
            // Returns true if an entry for a given key was found, and false if not.
            // Second argument uses the out qualifier to return the item when its found, or a null reference when its not found.
            string result;
            if(familyDictionary.TryGetValue("Saori", out result))
            {
                Console.WriteLine("Found Saori, here is her value: {0}.", result);
            }

            // A collection of distinct values. If you try tp add the same value twice, it will ignore the second addition which ensures uniqueness.
            ISet<string> set = new HashSet<string>();
            set.Add("one");
            set.Add("two");
            set.Add("three");
            set.Add("three");

            Console.WriteLine("Added 4 items to a HashSet but because of a duplicate, the count is {0}.", set.Count);

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
