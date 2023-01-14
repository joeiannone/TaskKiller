using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskKiller
{
    /// <summary>
    /// Interaction logic for Processes.xaml
    /// </summary>
    public partial class Processes : Page
    {

        public Processes()
        {
            InitializeComponent();
        }

        private void KillTask_Click(object sender, RoutedEventArgs e)
        {
            KillWindow killWindow;

            try
            {
                killWindow = new KillWindow(Process.GetProcessById((int)((Button)sender).Tag));
            }
            catch (Exception ex)
            {
                killWindow = new KillWindow(null);
            }
            
            killWindow.Show();
        }



    }
}
