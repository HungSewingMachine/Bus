using UnityEngine;

namespace BusJam
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private string path;

        private void Start()
        {
            var data = JsonReader.GetLevelData(path);
            
            mapGenerator.Generate(data.gridX, data.gridY);
        }

        // TODO: Need a timer preventing multiple call in Update
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log($"RedFlag mouse click!");
                SelectPassenger();
            }
        }

        /// <summary>
        /// Select Character by cast a ray from screen to world position
        /// </summary>
        private static void SelectPassenger()
        {
            var screenPos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out var hitInfo, 100, 1 << 7))
            {
                var visualPassenger = hitInfo.collider.GetComponent<VisualPassenger>();
                visualPassenger.TestSelect();
            }
        }
    }
}
