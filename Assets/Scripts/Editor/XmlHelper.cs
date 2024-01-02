using System;
using System.Xml.Linq;

namespace Assets.Scripts.Editor
{
    public static class XmlHelper
    {
        public static int GetAttributeInt(XElement e, string name)
        {
            var attr = e.Attribute(name);
            if (attr == null)
            {
                return default;
            }

            return int.TryParse(attr.Value, out var value) ? value : default;
        }

        public static string GetChildString(XElement e, string name)
        {
            var child = e.Element(name);
            if (child == null)
            {
                return default;
            }

            return child.Value;
        }

        public static TEnum GetChildEnum<TEnum>(XElement e, string name) where TEnum : struct
        {
            var child = e.Element(name);
            if (child == null)
            {
                return default;
            }

            return Enum.TryParse<TEnum>(child.Value, out var value) ? value : default;
        }
    }
}
