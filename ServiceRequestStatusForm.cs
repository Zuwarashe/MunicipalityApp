using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Municipality_App
{
    public partial class ServiceRequestStatusForm : Form
    {
        private BinarySearchTree<ServiceRequest> bst; // Binary Search Tree to manage service requests efficiently
        private List<ServiceRequest> serviceRequests; // List of service requests derived from issues

        private readonly string[] statusLevels = {
            "Pending", "In Progress", "Completed"
        }; // Predefined status levels for service requests

        private Random random; // Random generator to simulate status changes
        private Timer statusChangeTimer; // Timer to trigger periodic status updates for service requests

        public ServiceRequestStatusForm()
        {
            InitializeComponent();

            bst = new BinarySearchTree<ServiceRequest>(); // Initialize Binary Search Tree to store service requests
            serviceRequests = new List<ServiceRequest>(); // Initialize list to hold service requests
            random = new Random(); // Initialize random number generator

            // Convert issues from DataStorage to service requests
            foreach (var issue in DataStorage.Issues)
            {
                var request = new ServiceRequest(issue.Location, issue.Category, issue.Description, "Pending");
                serviceRequests.Add(request); // Add the request to the list
                bst.Insert(request); // Insert the service request into the Binary Search Tree
            }

            LoadServiceRequests(); // Load service requests into the ListView for display

            // Initialize and configure the timer to change statuses randomly every 10 seconds
            statusChangeTimer = new Timer();
            statusChangeTimer.Interval = 10000; // 10 seconds
            statusChangeTimer.Tick += StatusChangeTimer_Tick; // Event handler for timer tick
            statusChangeTimer.Start(); // Start the timer
        }

        private void StatusChangeTimer_Tick(object sender, EventArgs e)
        {
            // Select a random service request from the list
            var randomIndex = random.Next(serviceRequests.Count);
            var randomRequest = serviceRequests[randomIndex];

            // If the request is already "Completed", do nothing
            if (randomRequest.Status == "Completed")
                return;

            // Get the current status index in the predefined status levels
            var currentIndex = Array.IndexOf(statusLevels, randomRequest.Status);

            // Randomly select a new status, ensuring it is not the same as the current one
            string newStatus;
            do
            {
                newStatus = statusLevels[random.Next(statusLevels.Length)];
            } while (newStatus == randomRequest.Status); // Ensure status change is not to the same status

            // Update the status of the selected service request
            randomRequest.Status = newStatus;

            // Re-insert the updated request into the Binary Search Tree (BST)
            bst.Remove(randomRequest); // Remove old entry
            bst.Insert(randomRequest); // Insert updated entry

            LoadServiceRequests(); // Reload the ListView to show updated statuses
        }

        private void LoadServiceRequests()
        {
            listViewRequests.Items.Clear(); // Clear the ListView before adding updated items

            // Traverse the BST in-order to get sorted service requests
            var sortedRequests = new List<ServiceRequest>();
            bst.InOrderTraversal(request => sortedRequests.Add(request));

            // Add each service request to the ListView for display
            foreach (var request in sortedRequests)
            {
                var item = new ListViewItem(request.Location); // Use the Location as the first column
                item.SubItems.Add(request.Category); // Add category as second column
                item.SubItems.Add(request.Description); // Add description as third column
                item.SubItems.Add(request.Status); // Add current status as fourth column
                listViewRequests.Items.Add(item); // Add item to the ListView
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtIdentifier.Text.Trim(); // Get the search query from the user input

            // If the search field is empty, prompt the user for input
            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a RequestID to search for.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Clear the ListView to display only the search results //
            listViewRequests.Items.Clear();

            // Traverse the BST in-order to get sorted service requests
            var sortedRequests = new List<ServiceRequest>();
            bst.InOrderTraversal(request => sortedRequests.Add(request));

            // Filter the service requests by a partial match on Location (case-insensitive)
            var filteredRequests = sortedRequests.Where(request =>
                request.Location.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (filteredRequests.Count == 0) // If no results are found
            {
                MessageBox.Show("No service requests found with the given search term.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Display the filtered requests in the ListView
                foreach (var request in filteredRequests)
                {
                    var item = new ListViewItem(request.Location); // Location as the RequestID
                    item.SubItems.Add(request.Category); // Category
                    item.SubItems.Add(request.Description); // Description
                    item.SubItems.Add(request.Status); // Status
                    listViewRequests.Items.Add(item); // Add the item to the ListView
                }
            }
        }

        // Button click event handler to navigate back to the main form
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide the current form
            Form1 mainForm = new Form1(); // Create a new instance of the main form
            mainForm.Show(); // Show the main form
        }
    }
}
