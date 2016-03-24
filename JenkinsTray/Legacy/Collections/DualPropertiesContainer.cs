using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsTray.Utils.Collections
{
    [Obsolete]
    public class DualPropertiesContainer : IPropertiesContainer
    {
        string id;
        PropertiesContainer userProperties;
        PropertiesContainer defaultProperties;
        PropertiesContainer allProperties;
        PropertiesContainer[] writeEnabledContainers;

        // user properties: they can be changed
        internal IPropertiesContainer UserProperties
        {
            get { return userProperties; }
        }

        // default properties: they are read-only
        internal IPropertiesContainer DefaultProperties
        {
            get { return defaultProperties; }
        }

        // user+default properties: they are read-only
        internal IPropertiesContainer Properties
        {
            get { return allProperties; }
        }

        public DualPropertiesContainer(string id)
        {
            this.id = id;
            userProperties = new PropertiesContainer("User:" + id, true);
            defaultProperties = new PropertiesContainer("Default:" + id, true);
            allProperties = new PropertiesContainer("All:" + id, true);
            writeEnabledContainers = new PropertiesContainer[] { userProperties, allProperties };
        }

        delegate void PropertiesWriteActionDelegate(IPropertiesContainer properties);

        private void Apply(PropertiesWriteActionDelegate action)
        {
            foreach (PropertiesContainer container in writeEnabledContainers)
            {
                container.ReadOnly = false;
                action(container);
                container.ReadOnly = true;
            }
        }

        public string this[string key]
        {
            get { return allProperties[key]; }
            set
            {
                Apply(delegate(IPropertiesContainer properties)
                {
                    properties[key] = value;
                });
            }
        }

        public string GetStringValue(string key)
        {
            return allProperties.GetStringValue(key);
        }
        public string GetStringValue(string key, string defaultValue)
        {
            return allProperties.GetStringValue(key, defaultValue);
        }
        public string GetRequiredStringValue(string key)
        {
            return allProperties.GetRequiredStringValue(key);
        }
        public Int32? GetIntValue(string key)
        {
            return allProperties.GetIntValue(key);
        }
        public Int32 GetIntValue(string key, Int32 defaultValue)
        {
            return allProperties.GetIntValue(key, defaultValue);
        }
        public void SetIntValue(string key, int value)
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.SetIntValue(key, value);
            });
        }
        public Int32 GetRequiredIntValue(string key)
        {
            return allProperties.GetRequiredIntValue(key);
        }
        public float? GetFloatValue(string key)
        {
            return allProperties.GetFloatValue(key);
        }
        public float? GetFloatValue(string key, float defaultValue)
        {
            return allProperties.GetFloatValue(key, defaultValue);
        }
        public float GetRequiredFloatValue(string key)
        {
            return allProperties.GetRequiredFloatValue(key);
        }
        public bool? GetBoolValue(string key)
        {
            return allProperties.GetBoolValue(key);
        }
        public bool GetBoolValue(string key, bool defaultValue)
        {
            return allProperties.GetBoolValue(key, defaultValue);
        }
        public void SetBoolValue(string key, bool value)
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.SetBoolValue(key, value);
            });
        }
        public void RemoveValue(string key)
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.RemoveValue(key);
            });
        }

        public int GetGroupCount(string group)
        {
            return allProperties.GetGroupCount(group);
        }
        public string GetGroupStringValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupStringValue(group, groupId, key);
        }
        public string GetGroupStringValue(string group, int groupId, string key, string defaultValue)
        {
            return allProperties.GetGroupStringValue(group, groupId, key, defaultValue);
        }
        public string GetGroupRequiredStringValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupRequiredStringValue(group, groupId, key);
        }
        public Int32? GetGroupIntValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupIntValue(group, groupId, key);
        }
        public Int32? GetGroupIntValue(string group, int groupId, string key, Int32 defaultValue)
        {
            return allProperties.GetGroupIntValue(group, groupId, key, defaultValue);
        }
        public Int32 GetGroupRequiredIntValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupRequiredIntValue(group, groupId, key);
        }
        public float? GetGroupFloatValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupFloatValue(group, groupId, key);
        }
        public float? GetGroupFloatValue(string group, int groupId, string key, float defaultValue)
        {
            return allProperties.GetGroupFloatValue(group, groupId, key, defaultValue);
        }
        public float GetGroupRequiredFloatValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupRequiredFloatValue(group, groupId, key);
        }
        public bool? GetGroupBoolValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupBoolValue(group, groupId, key);
        }

        public bool GetGroupBoolValue(string group, int groupId, string key, bool defaultValue)
        {
            return allProperties.GetGroupBoolValue(group, groupId, key, defaultValue);
        }
        public void SetGroupCount(string group, int count)
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.SetGroupCount(group, count);
            });
        }
        public void SetGroupStringValue(string group, int groupId, string key, string value)
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.SetGroupStringValue(group, groupId, key, value);
            });
        }
        public void SetGroupIntValue(string group, int groupId, string key, int value)
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.SetGroupIntValue(group, groupId, key, value);
            });
        }
        public void SetGroupBoolValue(string group, int groupId, string key, bool value)
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.SetGroupBoolValue(group, groupId, key, value);
            });
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return allProperties.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return allProperties.GetEnumerator();
        }

        public void CopyPropertiesFrom(IPropertiesContainer properties)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool ReadOnly
        {
            get { throw new Exception("The method or operation is not implemented."); }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public void Clear()
        {
            Apply(delegate(IPropertiesContainer properties)
            {
                properties.Clear();
            });
        }
    }
}
