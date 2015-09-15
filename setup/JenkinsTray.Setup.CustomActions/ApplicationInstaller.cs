using System;
using System.Collections;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Windows.Forms;

namespace JenkinsTray.Setup.CustomActions
{
    [RunInstaller(true)]
    public class ApplicationInstaller : Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            Context.LogMessage("ApplicationInstaller[Install]");
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            Context.LogMessage("ApplicationInstaller[Uninstall]: stopping all instances of the application");

            Process[] processes = Process.GetProcessesByName("JenkinsTray");
            Context.LogMessage("ApplicationInstaller[Uninstall]: found " + processes.Length + " instances to stop");

            foreach (Process process in processes)
            {
                Context.LogMessage("ApplicationInstaller[Uninstall]: Stopping process id: " + process.Id);
                try
                {
                    process.Kill();
                }
                catch (Exception ex)
                {
                    Context.LogMessage("ApplicationInstaller[Uninstall]: Failed stopping process: "
                        + ex.Message + "\n" + ex.StackTrace);
                }
            }

            Context.LogMessage("ApplicationInstaller[Uninstall]: done");
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            Context.LogMessage("ApplicationInstaller[Commit]");
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            Context.LogMessage("ApplicationInstaller[Rollback]");
        }
    }
}
