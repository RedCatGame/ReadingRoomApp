using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ReadingRoomApp.Services
{
    public static class JsonHelper
    {
        public static void SaveToJsonFile<T>(List<T> data, string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<T> LoadFromJsonFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
    }
}