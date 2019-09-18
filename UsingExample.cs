using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Development
{
    class UsingExample
    {
        // Types that use unmanaged system resources should be explicitly released using the Dispose() method of the IDisposable interface.
        // Anytime you use types that have a Dispose method, it's your responsibility to release the resources by calling Dispose().
        // the best way to ensure that Dispose() always gets called is to utilize the using statement or try/finally block.
        // Once you allocate an unmanaged system resource object inside a using block statement, the C# compiler generates a try/finally block around each object.
        // A compiler error will be generated if you use the using statement with a variable of a type that does not implement the IDisposable interface.
        // The Dispose method also notifies the garbage collector  that the object no longer needs to be finalized.
        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
