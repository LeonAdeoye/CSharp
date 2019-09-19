using System;
using System.Collections.Generic;
using System.Collections;

namespace Development
{
    class Customer : IComparable<Customer>, IComparable, IComparer<Customer>
    {
        private readonly string name;
        
        public Customer(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }

        // IComparable<> interface contains one method: CompareTo().
        // IComparable<T> will be used by most of the new APIs in the .NET landscape.
        // However, some older APIs will use the classic IComparable interface.
        // Ergo when you implement IComparable<T>, you should also implement the class IComparable interface.
        #region IComparable<Customer> members
        public int CompareTo(Customer other)
        {
            return this.name.CompareTo(other.name);
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

        #region IComparer<Customer> members
        int IComparer<Customer>.Compare(Customer x, Customer y)
        {
            return x.name.Length.CompareTo(y.name.Length);
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
        }
    }
}
