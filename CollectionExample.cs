using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class CollectionExample
    {
        public static void main()
        {
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
            catch (Exception argumentException)
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
            if (familyDictionary.TryGetValue("Saori", out result))
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
        }


    }
}
