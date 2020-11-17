using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace StaffStorage
{
    public class XmlStorage : InMemoryStorage
    {
        private readonly String _filePath = System.AppDomain.CurrentDomain.BaseDirectory+"\\XmlRepoData.txt";
        public XmlStorage()
        {
            DeSerializeXml();
        }

        private void SerializeXml()
        {
            // Define the root element to avoid ArrayOfBranch
            var serializer = new XmlSerializer(typeof(List<Staff>),
                                               new XmlRootAttribute("Staffs"));
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            TextWriter writer = new StreamWriter(_filePath);
            serializer.Serialize(writer, this.staffs);
            writer.Close();
        }

        private void DeSerializeXml()
        {
            // Define the root element to avoid ArrayOfBranch
            var serializer = new XmlSerializer(typeof(List<Staff>), new XmlRootAttribute("Staffs"));
            if (File.Exists(_filePath))
            {
                TextReader textReader = new StreamReader(_filePath);
                this.staffs =  (List<Staff>)serializer.Deserialize(textReader);
                textReader.Close();
            }
        }

        ~XmlStorage()
        {
            SerializeXml();
        }

        public override void Dispose()
        {
            SerializeXml();
            base.Dispose();
        }
    }
}
