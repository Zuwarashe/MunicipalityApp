using System;

namespace Municipality_App
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public ServiceRequest(string location, string category, string description, string status)
        {
            Location = location;
            Category = category;
            Description = description;
            Status = status;
        }

        public int CompareTo(ServiceRequest other)
        {
            return string.Compare(Location, other.Location, StringComparison.OrdinalIgnoreCase);
        }
    }
}
