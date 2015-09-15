using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Common.Logging;
using System.Reflection;
using System.Threading;
using Jenkins.Tray.Utils.Logging;
using System.ComponentModel;
using System.Net;
using Jenkins.Tray.Utils.IO;
using Jenkins.Tray.Utils.Collections;

namespace Jenkins.Tray.BusinessComponents
{
    public class ApplicationUpdateService
    {
        public enum UpdateSource
        {
            User,
            Timer,
            Program
        }

        public delegate void NewVersionAvailableHandler(Version version, string installerUrl);
        public event NewVersionAvailableHandler NewVersionAvailable;

        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // every hour
        static readonly int DEFAULT_UPDATE_PERIOD = 60 * 60 * 1000;
        // 5 seconds delay for the 1st check on application start up
        static readonly int DEFAULT_UPDATE_DELAY = 5 * 1000;

        static readonly String URL = "https://raw.githubusercontent.com/zionyx/jenkins-tray/master/scripts/version.properties";

        static readonly String PROPERTY_VERSION_NUMBER = "version.number";
        static readonly String PROPERTY_INSTALLER_URL = "version.installerUrl";

        public ConfigurationService ConfigurationService { get; set; }

        Timer timer;
        int updatePeriod = DEFAULT_UPDATE_PERIOD;
        bool updating;
        bool timerEnabled;

        public ApplicationUpdateService()
        {
            timerEnabled = false;
            updating = false;
        }

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
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                DoCheckForUpdates(source);
            };
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
            catch { }
        }

        // returns true if an update was found, false otherwise
        private bool DoCheckForUpdates(UpdateSource source)
        {
            bool result = false;

            if (source == ApplicationUpdateService.UpdateSource.Timer &&
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
            bool result = false;
            logger.Info("Checking for updates from " + URL);

            // download the properties file
            WebClient webClient = new WebClient();
            String versionProperties = webClient.DownloadString(URL);

            // extract version details
            IPropertiesContainer properties = PropertiesFile.ReadProperties(versionProperties, "version.properties");
            string versionStr = properties[PROPERTY_VERSION_NUMBER];
            string installerUrl = properties[PROPERTY_INSTALLER_URL];

            Version version = new Version(versionStr);
            Version currentVersion = GetCurrentVersion();

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
            return new Version(FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion);
        }
    }
}
