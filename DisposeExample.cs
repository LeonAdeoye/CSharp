using System;
using System.Collections.Generic;

namespace Development
{
    class DisposeExample : IDisposable
    {
        // Enums are derived from value type.
        // The default initialization sets all objects to ZERO.
        // Ensure that zero is a valid state for value types.
        // If you don't then you force all users to explicitly initialize the value.
        public enum InnerSolarSystem
        {
            Sun = 0,
            Mercury = 1,
            Vrenus = 2,
            Earth = 3,
            Mars = 4
        }

        private InnerSolarSystem solar;

        private bool alreadyDisposed = false;

        public DisposeExample()
        {
        }

        public void Dispose()
        {
            // free unmanaged resources here.
            this.alreadyDisposed = true;
            GC.SuppressFinalize(this);
        }

        public void DoSomething()
        {
            if (alreadyDisposed)
                throw new ObjectDisposedException("DisposeExample", "Called DoSomething method on already disposed object.");
        }

        public static void main()
        {
            DisposeExample de = new DisposeExample();
            de.Dispose();
            try
            {
                de.DoSomething();
            }
            catch(ObjectDisposedException ode)
            {
                Console.WriteLine("Exception thrown: {0}", ode.Message);
            }
        }
    }
}
