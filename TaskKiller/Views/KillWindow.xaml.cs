﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskKiller.ViewModels;

namespace TaskKiller.Views
{
    /// <summary>
    /// Interaction logic for KillWindow.xaml
    /// </summary>
    public partial class KillWindow : Window
    {
        public KillWindow(Process? process)
        {
            InitializeComponent();

            try
            {
                Debug.WriteLine(typeof(Process).GetProperty("Id").GetValue(process, null));
                this.Title = $"Process ({process.Id}) - {process.ProcessName}";
                this.DataContext = new ProcessVM { process = process };
                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(HandleProcessExit);
            }
            catch (Exception)
            {
                this.DataContext = new ProcessVM { process = null };
            }
            
            
        }

        private void HandleProcessExit(object sender, System.EventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Close();
            });
        }
    }
}
