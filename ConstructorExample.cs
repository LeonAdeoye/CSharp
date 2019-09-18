using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Development
{
    class ConstructorExample
    {
        // Prefer member initialization to assignment statements.
        // Done before any code executes.
        private List<String> labels;
        private string name;

        // A static constructor executes before any other methods, variables, properties defined in the class are accessed for the first time.
        // You can use this function to initialize static variables, enforce the singleton pattern.
        // If you simply need to allocate a static member, use the initializer syntax, otherwise for complicated logic use the static constructor.
        // Exceptions are the most common reason to use the static constructor.
        static ConstructorExample()
        {

        }

        private ConstructorExample() : this(0, string.Empty)
        { }

        public ConstructorExample(int initialCount) : this(initialCount, string.Empty)
        {}

        public ConstructorExample(string name) : this(0, name)
        { }

        // When you find that multiple constructors contain the same logic, factor that logic into a common constructor instead.
        // You'll get the benefits of avoiding code duplication, and the constructor initializers generate much more efficient code.
        // And producing all the permutations using overloaded constructors would require many different constructor definitions.
        public ConstructorExample(int initialCount = 0, string name = "")
        {
            this.labels = (initialCount > 0) ?
                new List<String>(initialCount) :
                new List<string>();

            this.name = name;
        }
    }
}
