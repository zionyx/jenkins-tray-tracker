using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace JenkinsTray.Utils.BackgroundProcessing
{
    public class Process
    {
        // Summary:
        //     Occurs when System.ComponentModel.BackgroundWorker.RunWorkerAsync() is called.
        public event DoWorkEventHandler DoWork;
        //
        // Summary:
        //     Occurs when the background operation has completed, has been canceled, or
        //     has raised an exception.
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        string description;

        public string Description
        {
            get { return description; }
        }

        public Process(string description)
        {
            this.description = description;
        }

        internal void Run(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
                DoWork(this, e);
        }

        internal void OnCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (RunWorkerCompleted != null)
                RunWorkerCompleted(this, e);
        }
    }
}
