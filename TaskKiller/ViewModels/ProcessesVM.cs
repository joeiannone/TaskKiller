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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using TaskKiller.ViewModels.Commands;
using System.Reflection;
using System.Linq.Expressions;

namespace TaskKiller.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessesVM : INotifyPropertyChanged
    {

        private List<Process> _processes;
        private String _processCount;
        private string _searchString = String.Empty;
        private string _sortColumn;
        private ListSortDirection _lastSortDirection;

        public SortCommand SortCommand { get; set; }
        

        public event PropertyChangedEventHandler? PropertyChanged;


        public ProcessesVM()
        {
            UpdateProcesses();
            sortColumn = "Id";
            _lastSortDirection = ListSortDirection.Ascending;
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

        public async void UpdateProcesses()
        {
            await AsyncUpdateProcesses();
        }


        public async Task AsyncUpdateProcesses()
        {
            PropertyInfo? prop;
            try
            {
                prop = typeof(Process).GetProperty(sortColumn);
            }
            catch (ArgumentNullException ex)
            {
                prop = null;
            }

            processes = new List<Process>(Process.GetProcesses()
                .Where(p => p.ProcessName.ToLower().Contains(_searchString.ToLower()))
                .OrderBy(p => prop == null ? p.Id : prop.GetValue(p, null)));
        }


        public void UpdateSort(string column)
        {
            sortColumn = column;
            if (_lastSortDirection == ListSortDirection.Descending)
            {
                _lastSortDirection = ListSortDirection.Ascending;
            }
            else
            {
                _lastSortDirection = ListSortDirection.Descending;
            }
            UpdateProcesses();
        }






        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
