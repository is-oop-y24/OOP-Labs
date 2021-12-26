using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BackupsExtra
{
    public class SerializerToFile
    {
        public void SerializeToFile<T>(T obj, string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        public T DeserializeFromFile<T>(string filePath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
        }
    }
}