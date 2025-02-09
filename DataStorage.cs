using System.Collections.Generic;

namespace Municipality_App
{
    public static class DataStorage
    {
        // A static property to hold the issues list
        public static List<Issue> Issues { get; } = new List<Issue>();
    }
}
