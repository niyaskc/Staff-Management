using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StaffStorage
{
    public class XmlStorage : IRepository
    {
        private readonly String _filePath = System.AppDomain.CurrentDomain.BaseDirectory+"\\XmlRepoData.xml";
        private XmlSerializer _serializer;

        public XmlStorage()
        {
            _serializer = new XmlSerializer(typeof(List<Staff>), new Type[] { typeof(List<TeachingStaff>), typeof(List<AdministrativeStaff>), typeof(List<SupportStaff>) });
        }

        private bool SerializeXml(List<Staff> staffs)
        {
            TextWriter writer = new StreamWriter(_filePath);
            _serializer.Serialize(writer, staffs);
            writer.Close();
            return true;
        }

        private List<Staff> DeSerializeXml()
        {
            if (File.Exists(_filePath))
            {
                TextReader textReader = new StreamReader(_filePath);
                List<Staff> staffs = (List<Staff>)_serializer.Deserialize(textReader);
                textReader.Close();
                return staffs;
            }
            return null;
        }

        public bool AddStaff(Staff staff)
        {
            List<Staff> staffs = DeSerializeXml();
            if(staffs == null)
            {
                staffs = new List<Staff>();
            }
            staff.Id = staffs.Any() ? staffs.Max(s => s.Id) + 1 : 1;
            staffs.Add(staff);
            return SerializeXml(staffs);
        }

        public bool UpdateStaff(int id, Staff staff)
        {
            List<Staff> staffs = DeSerializeXml();
            if (staffs == null || staff == null)
            {
                return false;
            }
            int indexForUpdate = staffs.FindIndex(s => s.Id == id);
            if (indexForUpdate < 0) return false;
            staffs[indexForUpdate] = staff;
            return SerializeXml(staffs);
        }

        public bool DeleteStaff(int id)
        {
            List<Staff> staffs = DeSerializeXml();
            bool isFound = staffs.Remove(staffs.Find(s => s.Id == id));
            if (!isFound) return false;
            return SerializeXml(staffs);
        }

        public Staff ViewStaff(int id)
        {
            return DeSerializeXml()?.Find(s => s.Id == id);
        }

        public List<Staff> ViewAllStaff()
        {
            return DeSerializeXml();
        }
    }
}
