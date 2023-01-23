using System;
using System.Threading;
using System.Windows;

namespace TaskKiller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>The event mutex name.</summary>
        private const string UniqueEventName = "TaskKiller-1-8bc76a82-0586-48bb-b43a-1eb76f77c12c";

        /// <summary>The unique mutex name.</summary>
        private const string UniqueMutexName = "TaskKiller-1-7a2967b4-564a-44a7-83b7-955c571b0965";

        /// <summary>The event wait handle.</summary>
        private EventWaitHandle eventWaitHandle;

        /// <summary>The mutex.</summary>
        private Mutex mutex;


        /// <summary>The app on startup.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void AppOnStartup(object sender, StartupEventArgs e)
        {
            bool isOwned;
            this.mutex = new Mutex(true, UniqueMutexName, out isOwned);
            this.eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, UniqueEventName);

            // So, R# would not give a warning that this variable is not used.
            GC.KeepAlive(this.mutex);

            if (isOwned)
            {
                // Spawn a thread which will be waiting for our event
                var thread = new Thread(
                    () =>
                    {
                    while (this.eventWaitHandle.WaitOne())
                    {

                        Current.Dispatcher.BeginInvoke(
                            (Action)(() => {

                                var mainWindow = ((MainWindow)Current.MainWindow);

                                if (mainWindow.WindowState == WindowState.Minimized)
                                {
                                    mainWindow.WindowState = WindowState.Normal;
                                }

                                mainWindow.Activate();

                            }));

                            

                        }
                    });

                // It is important mark it as background otherwise it will prevent app from exiting.
                thread.IsBackground = true;

                thread.Start();
                return;
            }

            // Notify other instance so it could bring itself to foreground.
            this.eventWaitHandle.Set();

            // Terminate this instance.
            this.Shutdown();
        }

    }
}
