using System;
using System.IO;
using System.Reflection;
using Common.Logging;
using JenkinsTray.Utils.Collections;

namespace JenkinsTray.Utils.IO
{
    // reads a properties file
    [Obsolete]
    public class PropertiesFile : DualPropertiesContainer
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string filename;

        public PropertiesFile(string filename)
            : base(filename)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");
            this.filename = filename;
        }

        public static PropertiesFile ReadPropertiesFile(string filename)
        {
            var file = new PropertiesFile(filename);
            file.ReadProperties();
            return file;
        }

        public static PropertiesFile ReadPropertiesFileOrFail(string filename)
        {
            var file = new PropertiesFile(filename);
            file.ReadPropertiesOrFail();
            return file;
        }

        public static IPropertiesContainer ReadProperties(Stream stream, string id)
        {
            var properties = new PropertiesContainer(id);
            var streamReader = new StreamReader(stream);
            ParseStream(streamReader, properties);
            return properties;
        }

        public static IPropertiesContainer ReadProperties(string content, string id)
        {
            var properties = new PropertiesContainer(id);
            var reader = new StringReader(content);
            ParseStream(reader, properties);
            return properties;
        }

        public static bool CanReadFile(string metadataFile)
        {
            var exists = File.Exists(metadataFile);
            return exists;
        }

        public void WriteProperties()
        {
            WriteFile();
        }

        // Reads the properties from the file, and returns a boolean indicating wheter file was found or not.
        // The file used is the one provided as a constructor argument.
        public bool ReadProperties()
        {
            return ReadProperties(filename, true);
        }

        // Reads the properties from the file, and returns a boolean indicating wheter file was found or not
        public bool ReadProperties(string filename, bool isUserFile)
        {
            var exists = File.Exists(filename);
            if (exists)
            {
                // read properties
                var properties = isUserFile ? UserProperties : DefaultProperties;
                properties.ReadOnly = false;
                ParseFile(filename, properties);
                properties.ReadOnly = true;

                // copy to the "all" container
                Properties.ReadOnly = false;
                Properties.CopyPropertiesFrom(properties);
                Properties.ReadOnly = true;
            }
            else
                logger.Info("Properties file not found: " + filename);
            return exists;
        }

        // reads the properties from the file, and throws an exception if the file was not found
        public void ReadPropertiesOrFail()
        {
            var exists = ReadProperties();
            if (!exists)
                throw new Exception("Properties file not found: " + filename);
        }

        private void ParseFile(string filename, IPropertiesContainer properties)
        {
            if (logger.IsInfoEnabled)
                logger.Info("Parsing file: " + filename);

            var stream = File.OpenText(filename);
            ParseStream(stream, properties);
            stream.Close();
            stream.Dispose();
        }

        private static void ParseStream(TextReader stream, IPropertiesContainer properties)
        {
            string line;
            var i = 1;
            while ((line = stream.ReadLine()) != null)
                ParseLine(line, i++, properties);
        }

        private static void ParseLine(string line, int lineNumber, IPropertiesContainer properties)
        {
            if (line.Trim() == "" || line.StartsWith("#"))
                return;

            var index = line.IndexOf('=');
            if (index == -1)
            {
                logger.Error("Could not parse line " + lineNumber + ": " + line);
                throw new Exception("Could not parse line " + lineNumber + ": " + line);
            }

            var propKey = line.Substring(0, index).Trim();
            var propValue = line.Substring(index + 1).Trim();

            properties[propKey] = propValue;
        }

        private void WriteFile()
        {
            if (logger.IsInfoEnabled)
                logger.Info("Writing file: " + filename);

            var file = new StreamWriter(filename);
            foreach (var pair in UserProperties)
            {
                file.Write(pair.Key);
                file.Write("=");
                file.Write(pair.Value);
                file.WriteLine();
            }
            file.Flush();
            file.Close();
            file.Dispose();
            file = null;
        }
    }
}