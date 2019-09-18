using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class StringBuilderExample
    {
        public static void main()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("StringBuilder example: ");
            msg.Append("A problem has occurred.");
            msg.Append("This is the problem.");

            Console.WriteLine(msg.ToString());
        }
    }
}
