using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskKiller.ViewModel.Commands;

namespace TaskKiller.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessesVM : INotifyPropertyChanged
    {

        private List<Process> _processes;
        private String _processCount;
        private string _searchString = String.Empty;
        private bool _sortAsc;
        private string _sortColumn;

        public SortCommand SortCommand { get; set; }
        

        public event PropertyChangedEventHandler? PropertyChanged;


        public ProcessesVM()
        {
            UpdateProcesses();
            sortColumn = "ProcessName";
            sortAsc = true;
            SortCommand = new SortCommand(this);
            
        }
        public ObservableCollection<string> columns 
        { 
            get 
            { 
                return new ObservableCollection<string>() { "Id", "ProcessName" }; 
            } 
        }


        public List<Process> processes
        {
            get
            {
                return _processes;
            }
            set
            {
                _processes = value;
                OnPropertyChanged(nameof(processes));
            }
        }

        public String processCount
        {
            get
            {
                return _processCount;
            }
            set
            {
                _processCount = value;
                OnPropertyChanged(nameof(processCount));
            }
        }

        public bool sortAsc 
        { 
            get 
            {
                return _sortAsc; 
            } 
            set 
            {
                _sortAsc = value; 
            } 
        }

        public string sortColumn
        {
            get
            {
                return _sortColumn;
            }
            set
            {
                _sortColumn = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        

        public string searchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                UpdateProcesses();
            }
        }


        public void UpdateProcesses()
        {
            processes = new List<Process>(
                Process.GetProcesses()
                .Where(p => p.ProcessName.ToLower().Contains(_searchString.ToLower()) )); /*.OrderBy(p => p.GetType().GetProperty(sortColumn).GetValue(p)).ToList();*/
        }

        public void KillProcess(Process process)
        {
            //process.
            process.Kill();
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
