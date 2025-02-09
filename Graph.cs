using System.Collections.Generic;

namespace Municipality_App
{
    public class Graph
    {
        private Dictionary<string, List<Issue>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<Issue>>();
        }

        // Add an edge between a location/category and an issue
        public void AddEdge(string key, Issue issue)
        {
            if (!adjacencyList.ContainsKey(key))
            {
                adjacencyList[key] = new List<Issue>();
            }
            adjacencyList[key].Add(issue);
        }

        // Get connected issues by key (location/category)
        public List<Issue> GetConnectedIssues(string key)
        {
            return adjacencyList.ContainsKey(key) ? adjacencyList[key] : new List<Issue>();
        }

        // Depth-First Search
        public List<Issue> DFS(string key)
        {
            var visited = new HashSet<string>();
            var result = new List<Issue>();
            DFSHelper(key, visited, result);
            return result;
        }

        private void DFSHelper(string key, HashSet<string> visited, List<Issue> result)
        {
            if (!adjacencyList.ContainsKey(key) || visited.Contains(key)) return;

            visited.Add(key);
            foreach (var issue in adjacencyList[key])
            {
                result.Add(issue);
            }

            foreach (var neighbor in adjacencyList.Keys)
            {
                if (!visited.Contains(neighbor))
                {
                    DFSHelper(neighbor, visited, result);
                }
            }
        }
    }
}
