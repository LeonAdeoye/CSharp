using System;
using System.Collections.Generic;
using System.Collections;

namespace Development
{
    public class CustomerComparer : IComparer<Customer>, IComparer
    {
        // If Customer is not declared public then this methd won't be able access it. 
        public int Compare(Customer x, Customer y)
        {
            // Reverse sort if multiplied by -1.
            return x.Name.Length.CompareTo(y.Name.Length) * -1;
        }

        public int Compare(Object xo, object yo)
        {
            Customer x = null, y = null;

            if (xo is Customer)
                x = (Customer) xo;

            if (yo is Customer)
                y = (Customer)yo;

            // Reverse sort if multiplied by -1.
            return x.Name.Length.CompareTo(y.Name.Length) * -1;
        }
    }


    public class Customer : IComparable<Customer>, IComparable
    {
        public Customer(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Name;
        }

        // IComparable<> interface contains one method: CompareTo().
        // IComparable<T> will be used by most of the new APIs in the .NET landscape.
        // However, some older APIs will use the classic IComparable interface.
        // Ergo when you implement IComparable<T>, you should also implement the class IComparable interface.
        #region IComparable<Customer> members
        public int CompareTo(Customer other)
        {
            return this.Name.CompareTo(other.Name);
        }
        #endregion

        // IComparable takes parameters of type System.Object so you need to perform runtime checking on the argument to this function.
        // Simple backward compatibility and also easier to use for algorithms makign use of reflection.
        #region IComparable members
        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Customer))
                throw new ArgumentException("Argument is not a Customer");

            Customer other = (Customer)obj;
            return this.CompareTo(other);
        }
        #endregion


        // You can overload the standard relational operators.
        // They should make use of the type safe CompareTo method.
        // If you overload < operator you have to also overload the > operator.
        public static bool operator <(Customer left, Customer right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Customer left, Customer right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Customer left, Customer right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Customer left, Customer right)
        {
            return left.CompareTo(right) >= 0;
        }
        public static void main()
        {

            Customer c1 = new Customer("Horatio");
            Customer c2 = new Customer("Leon");
            Console.WriteLine("Horatio compareTo Leon: {0}", c1.CompareTo(c2));
            Console.WriteLine("Leon compareTo Horatio: {0}", c2.CompareTo(c1));
            Console.WriteLine("Leon compareTo Leon: {0}", c2.CompareTo(c2));

            // The overload of Console.WriteLine takes an array of System.Object references.
            // ints are values types and must be boxed so that they can be passed to the overload of the WriteLine method.
            // The first rule to avoid boxing: watch for implicit conversion to System.Object.
            // Another common inadverted implicit conversion of a value type to System.Object is when you place the value type in .NET 1.x collections.
            // Istead use generic collections. Be on the lookout for other constructs that implicitly convert values types to System.Object.
            Console.WriteLine("{0} < {1}: {2}", c1.ToString(), c2.ToString(), (c1 < c2).ToString());
            // Boxing and will occur in all three calls below.
            Console.WriteLine("{0} > {1}: {2}", c1, c2, c1 > c2);
            Console.WriteLine("{0} <= {1}: {2}", c1, c2, c1 <= c2);
            Console.WriteLine("{0} >= {1}: {2}", c1, c2, c1 >= c2);

            Customer[] array = new Customer[]
            {
                c1,
                c2,
                new Customer("Chloe"),
                new Customer("Ethan"),
                new Customer("Yuko")
            };

            Console.WriteLine("Normal Sorted array: ");
            Array.Sort(array);
            array.ForEach(Console.Write);

            Console.WriteLine("\nSorted array using string length comparer lambda expressionn: ");
            Array.Sort(array, (x,y) => x.Name.Length.CompareTo(y.Name.Length));
            array.ForEach(Console.Write);

            Console.WriteLine("\nSorted array using CustomerCustomer class which implements iComparer interface and does reverse length sort:");
            Array.Sort(array, new CustomerComparer());
            array.ForEach(Console.Write);
        }
    }
}
