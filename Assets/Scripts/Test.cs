using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        int[,] grid = {
            { 0, 1, 0, 0 },
            { 0, 0, 0, 1 },
            { 1, 0, 0, 0 },
            { 0, 0, 1, 0 }
        };

        Graph graph = new Graph(grid);
        var source = (3, 3);

        var shortestPath = graph.ShortestPathFromSource(source);

        if (shortestPath != null)
        {
            Debug.Log($"Shortest path from {source} to any reachable point in the first row:");
            foreach (var point in shortestPath)
            {
                Debug.Log(point);
            }
        }
        else
        {
            Debug.Log($"No reachable point from {source}.");
        }
    }
}
