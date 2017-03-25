using System;
using System.IO;
using Common.Logging;

namespace JenkinsTray.Utils.Logging
{
    public static class LoggingHelper
    {
        public static void LogError(ILog logger, Exception ex)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (ex == null)
                throw new ArgumentNullException("ex");

            logger.Error(ex.GetType().Name + " - " + ex.Message);
            var prefix = " ";
            while (ex.InnerException != null)
            {
                prefix += " ";
                logger.Error(prefix + ex.Message);
                ex = ex.InnerException;
            }
            logger.Error(ex.StackTrace);
        }

        // fail-safe
        public static void LogDirectoryContent(ILog logger, string directory)
        {
            try
            {
                DoLogDirectoryContent(logger, directory);
            }
            catch
            {
                if (logger != null)
                    logger.Error("Failed to log directory content");
            }
        }

        public static void DoLogDirectoryContent(ILog logger, string directory)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            var exists = Directory.Exists(directory);

            logger.Info("-- Logging directory content:");
            logger.Info("Directory: '" + directory + "'");
            logger.Info("Directory exists: " + exists);

            if (exists)
            {
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    logger.Info("File: '" + file + "' (size=" + fileInfo.Length + ")");
                }

                var directories = Directory.GetDirectories(directory);
                foreach (var subDirectory in directories)
                {
                    logger.Info("Directory: '" + subDirectory + "'");
                }
            }

            logger.Info("--");
        }
    }
}