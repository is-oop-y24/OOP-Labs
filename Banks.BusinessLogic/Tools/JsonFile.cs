using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Banks.BusinessLogic.Data
{
    public class JsonFile
    {
        private Dictionary<string, string> _dictionary;
        private const int _bufferSize = 1024;
        
        public JsonFile(string path)
        {
            using var stream = new FileStream(path, FileMode.Open);
            byte[] bytes = new byte[_bufferSize];
            stream.Read(bytes);
            string jsonString = Encoding.Default.GetString(bytes);
            _dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }

        public string this[string property] => _dictionary[property];
    }
}