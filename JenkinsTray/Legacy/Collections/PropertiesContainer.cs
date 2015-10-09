using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

using Common.Logging;
using System.Globalization;
using System.Text.RegularExpressions;

namespace JenkinsTray.Utils.Collections
{
    // reads a properties file
    [Obsolete]
    public class PropertiesContainer : IPropertiesContainer
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Regex REGEX_BOOLEAN_TRUE = new Regex("^( *)(true|1|yes)( *)$", RegexOptions.IgnoreCase);

        string id;
        IDictionary<string, string> properties;
        CultureInfo culture = new CultureInfo("en-US");
        bool readOnly;

        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        // returns the property or null if not found
        public string this[string key]
        {
            get { return GetStringValue(key); }
            set
            {
                if (readOnly)
                    throw new Exception("Cannot write to a read-only container");
                properties[key] = value;
            }
        }

        public PropertiesContainer(string id, bool readOnly)
        {
            this.id = id;
            this.readOnly = readOnly;
            properties = new Dictionary<string, string>();
        }

        public PropertiesContainer(string id)
            : this(id, false)
        {
        }

        public string GetStringValue(string key)
        {
            string res = null;
            properties.TryGetValue(key, out res);
            return res;
        }

        public string GetStringValue(string key, string defaultValue)
        {
            string res = GetStringValue(key);
            if (res != null)
                return res;
            return defaultValue;
        }

        public string GetRequiredStringValue(string key)
        {
            string value = GetStringValue(key);
            if (value != null)
                return value;
            throw new Exception("Required property '" + key + "' not found in container '" + id + "'");
        }

        public Int32? GetIntValue(string key)
        {
            string strValue = GetStringValue(key);
            if (strValue == null)
                return null;

            Int32 intValue;
            if (Int32.TryParse(strValue, out intValue) == false)
            {
                logger.Error("Failed reading Int32 property '" + key + "' in file '" + id + "'");
                throw new Exception("Failed reading Int32 property '" + key + "' in file '" + id + "'");
            }
            return intValue;
        }

        public Int32 GetIntValue(string key, Int32 defaultValue)
        {
            Int32? value = GetIntValue(key);
            if (value != null)
                return value.Value;
            return defaultValue;
        }

        public Int32 GetRequiredIntValue(string key)
        {
            Int32? value = GetIntValue(key);
            if (value != null)
                return value.Value;
            throw new Exception("Required property '" + key + "' not found in file '" + id + "'");
        }

        public void SetIntValue(string key, int value)
        {
            this[key] = value.ToString();
        }

        public float? GetFloatValue(string key)
        {
            string strValue = GetStringValue(key);
            if (strValue == null)
                return null;

            float floatValue;
            if (float.TryParse(strValue, NumberStyles.Float, culture, out floatValue) == false)
            {
                logger.Error("Failed reading float property '" + key + "' in file '" + id + "'");
                throw new Exception("Failed reading float property '" + key + "' in file '" + id + "'");
            }
            return floatValue;
        }

        public float? GetFloatValue(string key, float defaultValue)
        {
            float? value = GetFloatValue(key);
            if (value != null)
                return value.Value;
            return defaultValue;
        }

        public float GetRequiredFloatValue(string key)
        {
            float? value = GetFloatValue(key);
            if (value != null)
                return value.Value;
            throw new Exception("Required property '" + key + "' not found in file '" + id + "'");
        }

        public bool? GetBoolValue(string key)
        {
            string strValue = GetStringValue(key);
            if (strValue == null)
                return null;

            //if (strValue == "true" || strValue == "1" || strValue == "yes")
            //    return true;
            return REGEX_BOOLEAN_TRUE.IsMatch(strValue);
        }

        public bool GetBoolValue(string key, bool defaultValue)
        {
            bool? value = GetBoolValue(key);
            if (value != null)
                return value.Value;
            return defaultValue;
        }

        public void SetBoolValue(string key, bool value)
        {
            this[key] = value ? "true" : "false";
        }

        public void RemoveValue(string key)
        {
            properties.Remove(key);
        }

        public int GetGroupCount(string group)
        {
            int? count = GetIntValue(group + ".count");
            if (count.HasValue)
                return count.Value;
            return 0;
        }

        public string GetGroupStringValue(string group, int groupId, string key)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetStringValue(groupKey);
        }

        public string GetGroupStringValue(string group, int groupId, string key, string defaultValue)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetStringValue(groupKey, defaultValue);
        }

        public string GetGroupRequiredStringValue(string group, int groupId, string key)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetRequiredStringValue(groupKey);
        }

        public Int32? GetGroupIntValue(string group, int groupId, string key)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetIntValue(groupKey);
        }

        public Int32? GetGroupIntValue(string group, int groupId, string key, Int32 defaultValue)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetIntValue(groupKey, defaultValue);
        }

        public void SetGroupIntValue(string group, int groupId, string key, int value)
        {
            string groupKey = GetGroupKey(group, groupId, key);

            this[groupKey] = value.ToString();
        }

        public bool? GetGroupBoolValue(string group, int groupId, string key)
        {
            string strValue = GetGroupStringValue(group, groupId, key);

            if (strValue == null)
                return null;

            return REGEX_BOOLEAN_TRUE.IsMatch(strValue);
        }

        public bool GetGroupBoolValue(string group, int groupId, string key, bool defaultValue)
        {
            bool? value = GetGroupBoolValue(group, groupId, key);

            if (value != null)
                return value.Value;

            return defaultValue;
        }

        public void SetGroupBoolValue(string group, int groupId, string key, bool value)
        {
            string groupKey = GetGroupKey(group, groupId, key);

            this[groupKey] = value ? "true" : "false";
        }

        public Int32 GetGroupRequiredIntValue(string group, int groupId, string key)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetRequiredIntValue(groupKey);
        }

        public float? GetGroupFloatValue(string group, int groupId, string key)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetFloatValue(groupKey);
        }

        public float? GetGroupFloatValue(string group, int groupId, string key, float defaultValue)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetFloatValue(groupKey, defaultValue);
        }

        public float GetGroupRequiredFloatValue(string group, int groupId, string key)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            return GetRequiredFloatValue(groupKey);
        }

        public void SetGroupCount(string group, int count)
        {
            if (count < 1)
                throw new Exception("Count must be positive.");
            this[group + ".count"] = count.ToString();
        }

        public void SetGroupStringValue(string group, int groupId, string key, string value)
        {
            string groupKey = GetGroupKey(group, groupId, key);
            this[groupKey] = value;
        }

        private string GetGroupKey(string group, int groupId, string key)
        {
            return group + "[" + groupId + "]." + key;
        }

        #region IEnumerable<KeyValuePair<string,string>> Members

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        #endregion

        public void CopyPropertiesFrom(IPropertiesContainer properties)
        {
            foreach (KeyValuePair<string, string> pair in properties)
                this[pair.Key] = pair.Value;
        }

        public void Clear()
        {
            if (readOnly)
                throw new Exception("Cannot write to a read-only container");
            properties.Clear();
        }
    }
}
