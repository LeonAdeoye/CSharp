using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Development
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T item in sequence)
                action(item);
        }
    }

    class ReferencesExample
    {
        private readonly List<string> list;

        public ReferencesExample()
        {
            list = new List<string>();
            list.Add("Horatio");
            list.Add("Harper");
        }

        // The object returned implements an interface that prevents the member collection from beign modified ensuring encapsulation. 
        public IEnumerable<String> getListOfNamesUsingReadonlyInterface()
        {
            return list;
        }

        // The readonly collection does not contain methods or properties for modification.
        public ReadOnlyCollection<String> getListOfNamesUsingReadOnlyCollection()
        {
            return new ReadOnlyCollection<string>(list);
        }

        // A reference to the internal member collection is returned which destroys encapsulation.
        public List<String> getListOfNamesUsingModifiableReferenceType()
        {
            return list;
        }

        public static void main()
        {
            ReferencesExample re = new ReferencesExample();

            // Returns a reference type, caller can access member - gaping hole in your class encapsulation.
            var modifiableList = re.getListOfNamesUsingModifiableReferenceType();
            Console.WriteLine("Before modification: ");
            modifiableList.Add("Saori");

            Console.WriteLine("After modification: ");
            modifiableList.ForEach(Console.WriteLine);

            // Returns object that implements an interface that does not have any modification methods.
            // Using interfaces, you can choose to expose only ther methods and properties you want clients to use.
            var enumerableList = re.getListOfNamesUsingReadonlyInterface();
            // Uses extention method created above.
            enumerableList.ForEach(Console.WriteLine);

            // Returns read-only collection that does not support modification methods.
            var readonlyList = re.getListOfNamesUsingReadOnlyCollection();
            Console.WriteLine(readonlyList.Count);
        }
    }
}
