using System;
using System.Collections.Generic;
using System.Text;

namespace SoloDev.ManualMapper
{
    public class Configuration
    {
        public Func<Type, object> ServiceProvider { get; set; }

        public Configuration()
        {
            ServiceProvider = Activator.CreateInstance;
        }
    }
}
