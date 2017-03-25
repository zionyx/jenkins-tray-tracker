using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading;
using Common.Logging;
using JenkinsTray.Utils.IO;
using JenkinsTray.Utils.Logging;

namespace JenkinsTray.BusinessComponents
{
    public class ApplicationUpdateService
    {
        public delegate void NewVersionAvailableHandler(Version version, string installerUrl);

        public enum UpdateSource
        {
            User,
            Timer,
            Program
        }

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // every hour
        private static readonly int DEFAULT_UPDATE_PERIOD = 60*60*1000;
        // 5 seconds delay for the 1st check on application start up
        private static readonly int DEFAULT_UPDATE_DELAY = 5*1000;

        private static readonly string URL =
            "https://raw.githubusercontent.com/zionyx/jenkins-tray/master/scripts/version.properties";

        private static readonly string PROPERTY_VERSION_NUMBER = "version.number";
        private static readonly string PROPERTY_INSTALLER_URL = "version.installerUrl";

        private Timer timer;
        private bool timerEnabled;
        private readonly int updatePeriod = DEFAULT_UPDATE_PERIOD;
        private bool updating;

        public ApplicationUpdateService()
        {
            timerEnabled = false;
            updating = false;
        }

        public ConfigurationService ConfigurationService { get; set; }
        public event NewVersionAvailableHandler NewVersionAvailable;

        public void Initialize()
        {
            timer = new Timer(CheckForUpdates);
            EnableTimer(ConfigurationService.GeneralSettings.CheckForUpdates);
        }

        public void EnableTimer(bool enable)
        {
            if (timerEnabled && !enable)
            {
                //  Disable timer
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timerEnabled = false;
            }
            else if (!timerEnabled && enable)
            {
                //  Enable timer
                timer.Change(DEFAULT_UPDATE_DELAY, updatePeriod);
                timerEnabled = true;
            }
        }

        public void CheckForUpdates_Asynchronous(UpdateSource source)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate { DoCheckForUpdates(source); };
            worker.RunWorkerAsync();
        }

        public bool CheckForUpdates_Synchronous(UpdateSource source)
        {
            return DoCheckForUpdates(source);
        }

        private void CheckForUpdates(object state)
        {
            // ignore errors
            try
            {
                DoCheckForUpdates(UpdateSource.Timer);
            }
            catch
            {
            }
        }

        // returns true if an update was found, false otherwise
        private bool DoCheckForUpdates(UpdateSource source)
        {
            var result = false;

            if (source == UpdateSource.Timer &&
                ConfigurationService.GeneralSettings.CheckForUpdates == false)
            {
                logger.Info("Update check is already disabled in settings; stopping timer, from " + source);
                //  Methods should only have 1 return point!
                EnableTimer(false);
            }
            else
            {
                logger.Info("Running update check from " + source);

                lock (this)
                {
                    if (updating)
                    {
                        logger.Info("Already in update: skipping");
                        return false;
                    }
                    updating = true;
                }

                try
                {
                    result = DoCheckForUpdatesInternal();
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogError(logger, ex);
                    throw;
                }
                finally
                {
                    lock (this)
                    {
                        updating = false;
                    }
                }

                logger.Info("Done");
            }
            return result;
        }

        // returns true if an update was found, false otherwise
        private bool DoCheckForUpdatesInternal()
        {
            var result = false;
            logger.Info("Checking for updates from " + URL);

            // download the properties file
            var webClient = new WebClient();
            var versionProperties = webClient.DownloadString(URL);

            // extract version details
            var properties = PropertiesFile.ReadProperties(versionProperties, "version.properties");
            var versionStr = properties[PROPERTY_VERSION_NUMBER];
            var installerUrl = properties[PROPERTY_INSTALLER_URL];

            var version = new Version(versionStr);
            var currentVersion = GetCurrentVersion();

            logger.Info("Current version: " + currentVersion);
            logger.Info("Last version: " + version);
            logger.Info("Installer URL: " + installerUrl);

            if (version <= currentVersion)
            {
                logger.Info("No updates");
            }
            else
            {
                logger.Info("An update is available");

                // disable the timer
                EnableTimer(false);

                if (NewVersionAvailable != null)
                    NewVersionAvailable(version, installerUrl);

                result = true;
            }
            return result;
        }

        private Version GetCurrentVersion()
        {
            return new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
        }
    }
}