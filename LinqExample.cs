using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class LinqExample
    {
        public static void main()
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
                Console.WriteLine("{0} event starts {1} and ends at {2}.", item.Title, item.StartTime, item.EndTime);
            }

            DateTimeOffset newEventStart = new DateTimeOffset(2019, 9, 13, 20, 00, 20, 10, TimeSpan.Zero);
            TimeSpan newEventDuration = TimeSpan.FromHours(20);
            var overlapsAny = events.Any(ev => CalendarEvent.TimesOverlap(ev.StartTime, ev.Duration, newEventStart, newEventDuration));
            var overlapsAll = events.All(ev => CalendarEvent.TimesOverlap(ev.StartTime, ev.Duration, newEventStart, newEventDuration));
            Console.WriteLine("Any overlap exists: {0}, and All overlap exists: {1}.", overlapsAny, overlapsAll);

            Console.WriteLine("Sum hours: {0}", events.Sum(ev => ev.Duration.TotalHours));
            Console.WriteLine("Max: {0}", events.Max(ev => ev.Duration.TotalHours));
            Console.WriteLine("Min: {0}", events.Min(ev => ev.Duration.TotalHours));

            // The first argument is the seed value that will be built up as the aggregation runs.
            // The second argument is the delegate that will be invoked once for each item. 
            // It will be passed the current aggregated value (initially the seed value) and the current item.
            // Whatever this delegate returns becomes the new aggregated value, that will be passed in as the first argument when that delegate is called for the next item.
            Console.WriteLine("Sum minutes: {0}", events.Aggregate(0.0, (total, ev) => total + ev.Duration.TotalMinutes));
            var dict = events.ToDictionary(ev => ev.Title, ev => ev.Duration);
        }
    }
}
