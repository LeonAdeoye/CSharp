using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class DelegateExample
    {
        // Instead of specifying behaviour to be executed immediatley, the behaviour can somehow be "contained" in an object.
        // That object can be used like any other , and one operation you can perform on it is to execute/invoke the encapsulated action.
        // Alternatively, you can think of a delegate as a single method interface, and a delegate instance as an object implementing that interface.
        // Delegates are typically used when the code that wants to execute the actions doesn't know the details of what the actions should be.
        // A delegate type is a list of parameter types and a return type. It is derived from System.MulticastDelegate.
        // STEP ONE: DECLARE THE DELEGATE TYPE
        delegate string StringProcessor(string input);

        // STEP TWO: CREATE METHOD WHICH MATCHES DELEGATE SIGNATURE FOR THE DELEGATE'S ACTION. 
        public static string doSomething(string myString)
        {
            return myString.ToUpper();
        }

        public string doSomethingElse(string myString)
        {
            return myString.ToLower();
        }

        public string doNothing(string myString)
        {
            return myString;
        }

        public static void main()
        {
            // The word delegate is often confusing used to describe both the delegate type and the delegate instance.
            // But the distinction between the two is exactly the same as between any other type and instances of that type.


            // STEP THREE: CREATE AN INSTANCE OF THE DELEGATE TYPE, SPECIFYING WHICH METHOD (THE ACTION) WILL BE EXECUTED WHEN THE DELEGATE INSTANCE IS INVOKED.
            StringProcessor sp1 = new StringProcessor(doSomething);
            // When the ACTION in an instance method, you need an instance of the type.  This object instance is called the TARGET of the ACTION.
            // When the delegate instance is invoked, the ACTION will be invoked on the TARGET.
            // A delegate instance will prevent its target from being garbage collected if the delegate instance itself is collected.
            // This will result in memory leaks: a long-lived object indirectly holds a reference to short-lived one, prolonging its lifetime.
            DelegateExample de = new DelegateExample();
            StringProcessor sp2 = new StringProcessor(de.doSomethingElse);

            // STEP FOUR: INVOKE THE DELEGATE INSTANCE
            Console.WriteLine("Invoking delegate instance sp1 returns {0}", sp1.Invoke("leon"));
            // Invoking a delegate without using an explicit call of the Invoke method.
            Console.WriteLine("Invoking delegate instance sp2 returns {0}", sp2("LEON"));
            sp2 += sp1;
            Console.WriteLine("Invoking multicasted delegate instance sp2 returns {0}", sp2("LEON"));
            StringProcessor sp3 = new StringProcessor(de.doSomethingElse);
        }
    }
}
