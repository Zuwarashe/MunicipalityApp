// File: Issue.cs
using System;

namespace Municipality_App
{
    // Class to represent a reported issue
    public class Issue
    {
        public int RequestID { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaPath { get; set; }
    }
}
