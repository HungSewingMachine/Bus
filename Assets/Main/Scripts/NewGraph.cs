using System.Collections.Generic;
using UnityEngine;

namespace BusJam
{
    public class NewGraph : MonoBehaviour
    {
        private Cell[,] grid;
        private int rows;
        private int cols;
        private Dictionary<(int, int), List<(int, int)>> graph;

        public NewGraph(Cell[,] g)
        {
            grid = g;
            rows = grid.GetLength(0);
            cols = grid.GetLength(1);
            graph = new Dictionary<(int, int), List<(int, int)>>();

            BuildGraph();
        }

        /// <summary>
        /// Check valid cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool IsValidCell(int row, int col)
        {
            var isIndexValid = row >= 0 && row < rows && col >= 0 && col < cols;
            return isIndexValid && IsCellWalkable(row, col);
        }

        private bool IsCellWalkable(int row, int col)
        {
            return grid[row, col].IsAvailable;
        }

        /// <summary>
        /// Create edge. This function depend on what kind of problem you have
        /// In this project the rule is you only move in up, down, left, right
        /// </summary>
        private void BuildGraph()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var neighbors = new List<(int, int)>();

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

        /// <summary>
        /// Method for get the shortest exist path
        /// </summary>
        /// <param name="source">starting point defined by rowIndex and colIndex</param>
        /// <returns>List of cell (defined by rowIndex and colIndex) that form the path if it exists, if not return null</returns>
        public List<(int, int)> ShortestPathFromSource((int, int) source)
        {
            var isSourceAtFirstRow = source.Item1 == 0;
            if (isSourceAtFirstRow)
            {
                return new List<(int, int)>() { source };
            }

            var parent = new Dictionary<(int, int), (int, int)>();
            var queue = new Queue<(int, int)>();
            var visited = new HashSet<(int, int)>();

            queue.Enqueue(source);
            visited.Add(source);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var neighbor in graph[current])
                {
                    var cellRow = neighbor.Item1;
                    var cellCol = neighbor.Item2;
                    if (IsCellWalkable(cellRow, cellCol) && !visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parent[neighbor] = current;

                        // If the current neighbor is in the first row of the grid,
                        // it's considered as a destination point
                        var isFirstRow = cellRow == 0;
                        if (isFirstRow)
                        {
                            // Reconstruct path
                            var path = new List<(int, int)>();
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

        /// <summary>
        /// A cell know if a character in it or not. So when character move, cell should remove one.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="p"></param>
        public void RemoveCharacterAtCell(int row, int col, Passenger p)
        {
            var cell = grid[row, col];
            cell.passengers.Remove(p);
        }
    }
}
