using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Jenkins.TrayTracker.Utils.Collections
{
    // reads a properties file
    [Obsolete]
    public interface IPropertiesContainer : IEnumerable<KeyValuePair<string, string>>
    {
        bool ReadOnly { get; set; }

        string this[string key] { get; set; }

        string GetStringValue(string key);
        string GetStringValue(string key, string defaultValue);
        string GetRequiredStringValue(string key);
        Int32? GetIntValue(string key);
        Int32 GetIntValue(string key, Int32 defaultValue);
        Int32 GetRequiredIntValue(string key);
        void SetIntValue(string key, int value);
        float? GetFloatValue(string key);
        float? GetFloatValue(string key, float defaultValue);
        float GetRequiredFloatValue(string key);
        bool? GetBoolValue(string key);
        bool GetBoolValue(string key, bool defaultValue);
        void SetBoolValue(string key, bool value);
        void RemoveValue(string key);

        int GetGroupCount(string group);
        string GetGroupStringValue(string group, int groupId, string key);
        string GetGroupStringValue(string group, int groupId, string key, string defaultValue);
        string GetGroupRequiredStringValue(string group, int groupId, string key);
        Int32? GetGroupIntValue(string group, int groupId, string key);
        Int32? GetGroupIntValue(string group, int groupId, string key, Int32 defaultValue);
        Int32 GetGroupRequiredIntValue(string group, int groupId, string key);
        float? GetGroupFloatValue(string group, int groupId, string key);
        float? GetGroupFloatValue(string group, int groupId, string key, float defaultValue);
        float GetGroupRequiredFloatValue(string group, int groupId, string key);
        bool? GetGroupBoolValue(string group, int groupId, string key);
        bool GetGroupBoolValue(string group, int groupId, string key, bool defaultValue);
        void SetGroupCount(string group, int count);
        void SetGroupStringValue(string group, int groupId, string key, string value);
        void SetGroupIntValue(string group, int groupId, string key, int value);
        void SetGroupBoolValue(string group, int groupId, string key, bool value);

        void CopyPropertiesFrom(IPropertiesContainer properties);

        void Clear();
    }
}
