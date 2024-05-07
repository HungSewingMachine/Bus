using UnityEngine;

namespace BusJam
{
    /// <summary>
    /// Use to set up all visual of main game
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        
        [Tooltip("This param is the distance offset such that all cell are display nicely")]
        public float zOffset              = -4;
        [Tooltip("The horizontal distance between cells")]
        public float xDistanceBetweenCell = 1.5f;
        [Tooltip("The vertical distance between cells. Negative means it go opposite z axis")]
        public float zDistanceBetweenCell = -1.5f;

        private float xOffset;

        /// <summary>
        /// Main method for spawning visual of game
        /// </summary>
        /// <param name="gridX">Number of grids in X axis - or number of Column</param>
        /// <param name="gridY">Number of grids in Y axis - or number of Row</param>
        public void Generate(int gridX, int gridY)
        {
            xOffset = -(xDistanceBetweenCell * (gridX - 1)) / 2;
            // xOffset = -(xDistanceBetweenCell * (9 - 1)) / 2;
            Debug.Log($"RedFlag number of x grid: {gridX} distance: {xDistanceBetweenCell} offset: {xOffset}");

            for (int i = 0; i < gridY; i++)
            {
                for (int j = 0; j < gridX; j++)
                {
                    var go = Instantiate(cellPrefab, GetCellPositionInGrid(i, j), Quaternion.identity);
                    go.name = $"Row: {i} Column: {j}";
                }
            }
        }

        /// <summary>
        /// This function should be called after reading level data is completed and the xOffset is set. Which means called after Generate()
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Vector3 GetCellPositionInGrid(int row, int col)
        {
            return new Vector3(xOffset + col * xDistanceBetweenCell, 0, zOffset + row * zDistanceBetweenCell);
        }
    }
}
