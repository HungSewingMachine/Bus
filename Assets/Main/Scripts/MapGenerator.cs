using UnityEngine;

namespace BusJam
{
    /// <summary>
    /// Use to setup all visual of main game
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
        
        public string path;

        private float xOffset;
        
        private void Start()
        {
            var data = JsonReader.GetLevelData(path);
            xOffset = -(xDistanceBetweenCell * (data.gridX - 1)) / 2;
            // xOffset = -(xDistanceBetweenCell * (9 - 1)) / 2;
            Debug.Log($"RedFlag number of x grid: {data.gridX} distance: {xDistanceBetweenCell} offset: {xOffset}");

            for (int i = 0; i < data.gridY; i++)
            {
                for (int j = 0; j < data.gridX; j++)
                {
                    var go = Instantiate(cellPrefab, GetCellPositionInGrid(i, j), Quaternion.identity);
                    go.name = $"Row: {i} Column: {j}";
                }
            }
        }

        /// <summary>
        /// This function should be called after reading level data is completed and the xOffset is set
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
