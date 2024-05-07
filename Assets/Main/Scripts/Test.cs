using NaughtyAttributes;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int row;
    public int col;

    private Graph cachedGraph;

    private void Start()
    {
        int[,] grid = {
            { 1, 1, 1, 1 },
            { 1, 1, 0, 1 },
            { 1, 0, 1, 0 },
            { 0, 1, 1, 1 }
        };

        cachedGraph = new Graph(grid);
    }

    [Button]
    public void PrintGraph()
    {
        cachedGraph.PrintGraph();
    }

    [Button]
    public void StartNode()
    {
        var source = (row, col);

        var shortestPath = cachedGraph.ShortestPathFromSource(source);

        if (shortestPath != null)
        {
            Debug.Log($"Shortest path from {source} to any reachable point in the first row:");
            foreach (var point in shortestPath)
            {
                Debug.Log(point);
            }
            cachedGraph.Remove(row, col);
        }
        else
        {
            Debug.Log($"No reachable point from {source}.");
        }
    }
}
