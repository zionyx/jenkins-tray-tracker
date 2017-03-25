using System.ComponentModel;

namespace JenkinsTray.Utils.BackgroundProcessing
{
    public class Process
    {
        public Process(string description)
        {
            Description = description;
        }

        public string Description { get; }

        // Summary:
        //     Occurs when System.ComponentModel.BackgroundWorker.RunWorkerAsync() is called.
        public event DoWorkEventHandler DoWork;
        //
        // Summary:
        //     Occurs when the background operation has completed, has been canceled, or
        //     has raised an exception.
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

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