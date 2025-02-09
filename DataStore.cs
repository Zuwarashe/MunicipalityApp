using System;
using System.Collections.Generic;

namespace Municipality_App
{
    public static class DataStore
    {
        // List to hold all service requests
        

        // List to hold all issues (for conversion to service requests)
        public static List<Issue> Issues { get; } = new List<Issue>();
    }
}
