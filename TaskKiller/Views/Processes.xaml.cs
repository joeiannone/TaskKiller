using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace TaskKiller.Views
{
    /// <summary>
    /// Interaction logic for Processes.xaml
    /// </summary>
    public partial class Processes : Page
    {

        private Process? _selectedItem;
        
        /// <summary>
        /// 
        /// </summary>
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
