using System;
using System.Xml;

namespace Logobot2_0
{
    public class LogobotSettings
    {
        public string username { get; set; }

        public string password { get; set; }

        public bool noLogo { get; set; }

        public string path { get; set; }

        public XmlDocument userSettingsXML = null;

        public LogobotSettings()
        {

        }

        public void Create()
        {
            userSettingsXML = new XmlDocument();

            // Initialize settings document
            userSettingsXML.AppendChild(userSettingsXML.CreateNode(XmlNodeType.XmlDeclaration, null, null));
            userSettingsXML.AppendChild(userSettingsXML.CreateElement("Settings"));
        }

        public void Save(string filename)
        {
            userSettingsXML.Save(filename);
        }

        public void Load(string filename)
        {
            userSettingsXML = new XmlDocument();
            userSettingsXML.Load(filename);
        }

        public string Read(string key, string defaultValue)
        {
            XmlNode node = userSettingsXML.DocumentElement.SelectSingleNode(key);
            if (node != null && !String.IsNullOrEmpty(node.InnerText))
            {
                return node.InnerText;
            }
            else
            {
                return defaultValue;
            }
        }

        public bool Read(string key, bool defaultValue)
        {
            XmlNode node = userSettingsXML.DocumentElement.SelectSingleNode(key);
            if (node != null && !String.IsNullOrEmpty(node.InnerText))
            {
                return Boolean.Parse(node.InnerText);
            }
            else
            {
                return defaultValue;
            }
        }

        public void Update(string key, string value)
        {
            XmlNode node = userSettingsXML.DocumentElement.SelectSingleNode(key);
            node.InnerText = value.ToString();
        }

        public void Update(string key, bool value)
        {
            XmlNode node = userSettingsXML.DocumentElement.SelectSingleNode(key);
            node.InnerText = value.ToString();
        }

        public void Write(string key, string value)
        {
            XmlElement elem = userSettingsXML.CreateElement(key);
            elem.InnerText = value.ToString();
            userSettingsXML.DocumentElement.AppendChild(elem);
        }

        public void Write(string key, bool value)
        {
            XmlElement elem = userSettingsXML.CreateElement(key);
            elem.InnerText = value.ToString();
            userSettingsXML.DocumentElement.AppendChild(elem);
        }
    }
}
