using System;
using System.Diagnostics;
using System.Windows;
using TaskKiller.ViewModels;

namespace TaskKiller.Views
{
    /// <summary>
    /// Interaction logic for KillWindow.xaml
    /// </summary>
    public partial class KillWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        public KillWindow(Process? process)
        {
            InitializeComponent();

            try
            {
                // try to set window title and data context for window
                this.Title = $"Process ({process.Id}) - {process.ProcessName}";
                this.DataContext = new ProcessVM { process = process };

                // try to create event handler to handle closing the window when the process exits
                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(HandleProcessExit);
            }
            catch (Exception)
            {
                this.DataContext = new ProcessVM { process = null };
            }
            
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleProcessExit(object? sender, System.EventArgs e)
        {
            CloseWindow();
        }

        /// <summary>
        /// Close this window
        /// </summary>
        private void CloseWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Close();
            });
        }
    }
}
