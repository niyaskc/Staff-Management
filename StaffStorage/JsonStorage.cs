using Newtonsoft.Json;
using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StaffStorage
{
    public class JsonStorage : InMemoryStorage
    {
        private readonly String _filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\JsonRepoData.txt";
        private JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto , NullValueHandling = NullValueHandling.Ignore};

        public JsonStorage()
        {
            DeSerializeJson();
        }

        private void SerializeJson()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            String json = JsonConvert.SerializeObject(staffs, Formatting.Indented, settings);
            File.WriteAllText(_filePath, json);
        }

        private void DeSerializeJson()
        {
            if (File.Exists(_filePath))
            {
                this.staffs = JsonConvert.DeserializeObject<List<Staff>>(File.ReadAllText(_filePath), settings);
            }
        }

        ~JsonStorage()
        {
            SerializeJson();
        }

        public override void Dispose()
        {
            SerializeJson();
            base.Dispose();
        }

    }
}
