using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using TaskKiller.ViewModels;

namespace TaskKiller.Views
{
    /// <summary>
    /// Interaction logic for Processes.xaml
    /// </summary>
    public partial class Processes : Page
    {

        private Process? _selectedItem;
        
        public Processes()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private int? selectedIndex
        {
            get
            {
                if (_selectedItem == null)
                {
                    return null;
                }
                else
                {
                    List<Process> itemsSourceProcesses = ListView_Processes.ItemsSource as List<Process>;
                    return itemsSourceProcesses.IndexOf(itemsSourceProcesses.FirstOrDefault(p => p.Id == _selectedItem.Id));
                }
;            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_Processes_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (selectedIndex != null)
            {
                ListView_Processes.SelectedIndex = (int)selectedIndex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_Processes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Process? selectedItem = ListView_Processes.SelectedItem as Process;
            if (selectedItem != null)
            {
                _selectedItem = selectedItem;
            }
        }

    }
}
