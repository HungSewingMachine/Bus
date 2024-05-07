using System.IO;
using UnityEngine;

namespace BusJam
{
    public static class JsonReader
    {
        public static SavedData GetLevelData(string dataPath)
        {
            var result = new SavedData();
            var filePath = Path.Combine(Application.dataPath, dataPath);
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the JSON file
                var jsonString = File.ReadAllText(filePath);

                // Parse the JSON data into a JSON object
                result = JsonUtility.FromJson<SavedData>(jsonString);

                Debug.Log($"RedFlag data {JsonUtility.ToJson(result)}");
            }
            else
            {
                Debug.LogError("File not found: " + filePath);
            }

            return result;
        }
    }
}