#if DEBUG
// only define this if needed in debug
//#define DONT_CATCH_EXCEPTION
#endif

using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using Common.Logging;
using JenkinsTray.Utils.Logging;

namespace JenkinsTray.Utils.BackgroundProcessing
{
    public static class BackgroundProcessExecutor
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Execute(Process process)
        {
#if false
            if (Thread.CurrentThread.IsBackground)
                throw new Exception("Should not run in background!");
            int fgThreadId = Thread.CurrentThread.ManagedThreadId;
#endif

            if (logger.IsDebugEnabled)
                logger.Debug("Running process: " + process.Description);

            // run that process
            var bgWorker = new BackgroundWorker();
            var errorHolder = new ErrorHolder();
            bgWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
                               {
#if DEBUG
                                   if (Thread.CurrentThread.IsBackground == false)
                                       throw new Exception("Should run in background!");
#endif

#if DONT_CATCH_EXCEPTION
                process.Run(sender, e);
#else
                                   // here we catch exceptions because otherwise, Visual Studio breaks,
                                   // even though the BackgroundWorker itself catches the exceptions!
                                   try
                                   {
                                       process.Run(sender, e);
                                   }
                                   catch (Exception error)
                                   {
                                       errorHolder.Error = error;
                                   }
#endif
                               };
            bgWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                                           {
#if false
                if (Thread.CurrentThread.IsBackground)
                    throw new Exception("Should not run in background!");
                if (Thread.CurrentThread.ManagedThreadId != fgThreadId)
                    throw new Exception("RunWorkerCompleted called in another thread");
#endif

                                               // replace the args with one containing the caught error (if any)
                                               e = new RunWorkerCompletedEventArgs(e.Result, errorHolder.Error,
                                                                                   e.Cancelled);

                                               if (logger.IsDebugEnabled)
                                                   logger.Debug("Done running process: " + process.Description);
                                               if (e.Error != null)
                                                   LoggingHelper.LogError(logger, e.Error);

                                               process.OnCompleted(sender, e);
                                           };
            bgWorker.RunWorkerAsync();
        }
    }
}