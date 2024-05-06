using UnityEngine;

namespace BusJam
{
    public class MapGenerator : MonoBehaviour
    {
        public string path; 
        
        private void Start()
        {
            var data = JsonReader.GetLevelData(path);
            Debug.Log($"RedFlag got data!");
        }
    }
}
