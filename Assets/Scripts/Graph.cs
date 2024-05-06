using System.Collections.Generic;

public class Graph
{
    private int[,] grid;
    private int rows;
    private int cols;
    private Dictionary<(int, int), List<(int, int)>> graph;

    public Graph(int[,] grid)
    {
        this.grid = grid;
        this.rows = grid.GetLength(0);
        this.cols = grid.GetLength(1);
        this.graph = new Dictionary<(int, int), List<(int, int)>>();

        BuildGraph();
    }

    private bool IsValidCell(int row, int col)
    {
        return row >= 0 && row < rows && col >= 0 && col < cols && grid[row, col] == 0;
    }

    private void BuildGraph()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                List<(int, int)> neighbors = new List<(int, int)>();

                if (IsValidCell(i - 1, j))
                    neighbors.Add((i - 1, j)); // Up
                if (IsValidCell(i + 1, j))
                    neighbors.Add((i + 1, j)); // Down
                if (IsValidCell(i, j - 1))
                    neighbors.Add((i, j - 1)); // Left
                if (IsValidCell(i, j + 1))
                    neighbors.Add((i, j + 1)); // Right

                graph[(i, j)] = neighbors;
            }
        }
    }

    public List<(int, int)> ShortestPathFromSource((int, int) source)
    {
        if (source.Item1 == 0)
        {
            return new List<(int, int)>() { source };
        }

        Dictionary<(int, int), (int, int)> parent = new Dictionary<(int, int), (int, int)>();
        Queue<(int, int)> queue = new Queue<(int, int)>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>();

        queue.Enqueue(source);
        visited.Add(source);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbor in graph[current])
            {
                if (grid[neighbor.Item1,neighbor.Item2] == 0 && !visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    parent[neighbor] = current;

                    // If the current neighbor is in the first row of the grid,
                    // it's considered as a destination point
                    if (neighbor.Item1 == 0)
                    {
                        // Reconstruct path
                        List<(int, int)> path = new List<(int, int)>();
                        var node = neighbor;
                        while (parent.ContainsKey(node))
                        {
                            path.Insert(0, node);
                            node = parent[node];
                        }
                        path.Insert(0, source);
                        return path;
                    }
                }
            }
        }

        // No reachable destination found
        return null;
    }
}
