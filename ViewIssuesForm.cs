using System;
using System.IO;
using System.Windows.Forms;

namespace Municipality_App
{
    public partial class ViewIssuesForm : Form
    {
        public ViewIssuesForm()
        {
            InitializeComponent();
        }

        private void ViewIssuesForm_Load(object sender, EventArgs e)
        {
            richTextBoxIssues.Clear();

            // Fetch issues from the shared data storage
            foreach (var issue in DataStorage.Issues)
            {
                string issueDetails = $"Location: {issue.Location}\n" +
                                      $"Category: {issue.Category}\n" +
                                      $"Description: {issue.Description}\n" +
                                      $"Media Path: {issue.MediaPath}\n\n";

                richTextBoxIssues.AppendText(issueDetails);
            }
        }

        private void richTextBoxIssues_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBoxIssues.SelectedText.Contains("Media Path:"))
            {
                string selectedText = richTextBoxIssues.SelectedText;
                string mediaPath = ExtractMediaPath(selectedText);

                if (!string.IsNullOrEmpty(mediaPath) && File.Exists(mediaPath))
                {
                    pictureBoxIssue.Image = System.Drawing.Image.FromFile(mediaPath);
                }
                else
                {
                    pictureBoxIssue.Image = null;
                }
            }
        }

        private string ExtractMediaPath(string text)
        {
            var parts = text.Split(new[] { "Media Path:" }, StringSplitOptions.None);
            return parts.Length > 1 ? parts[1].Trim() : string.Empty;
        }
    }
}
