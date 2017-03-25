using System.Xml;

namespace JenkinsTray.Utils
{
    public static class XmlUtils
    {
        public static string SelectSingleNodeText(XmlNode xml, string xpath)
        {
            var node = xml.SelectSingleNode(xpath);
            if (node == null)
                return null;
            return node.InnerText;
        }

        public static bool? SelectSingleNodeBoolean(XmlNode xml, string xpath)
        {
            var node = xml.SelectSingleNode(xpath);
            if (node == null)
                return null;
            return node.InnerText == "true";
        }
    }
}