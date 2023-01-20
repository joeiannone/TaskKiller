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
using System.Security.AccessControl;

namespace TaskKiller.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessesVM : INotifyPropertyChanged
    {

        private List<Process> _processes;
        private string _searchString = String.Empty;
        private string _sortColumn;
        private ListSortDirection _lastSortDirection;

        public SortCommand SortCommand { get; set; }
        public ProcessWindowCommand ProcessWindowCommand { get; set; }
        

        public event PropertyChangedEventHandler? PropertyChanged;


        public ProcessesVM()
        {   
            // initialize processes
            _processes = new List<Process>(Process.GetProcesses());
            _sortColumn = "WorkingSet64";
            _lastSortDirection = ListSortDirection.Descending;

            SortCommand = new SortCommand(this);
            ProcessWindowCommand = new ProcessWindowCommand(this);
            
            // update with filters and sort
            UpdateProcesses();
            _ = RunInBackground(TimeSpan.FromSeconds(5), () => { UpdateProcesses(); });


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
        

        public string sortColumn
        {
            get
            {
                return _sortColumn;
            }
            set
            {
                _sortColumn = value;
                OnPropertyChanged(nameof(sortColumn));
                OnPropertyChanged(nameof(sortSelectionString));
            }
        }

        public string sortSelectionString
        {
            get
            {
                if (_lastSortDirection == ListSortDirection.Ascending)
                {
                    return $"{sortColumn} [ASC]";
                }
                else
                {
                    return $"{sortColumn} [DESC]";
                }
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

        async Task RunInBackground(TimeSpan timeSpan, Action action)
        {
            var periodicTimer = new PeriodicTimer(timeSpan);
            while (await periodicTimer.WaitForNextTickAsync())
            {
                action();
            }
        }


        


        public void UpdateProcesses()
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

            IEnumerable<Process> query;

            if (_lastSortDirection == ListSortDirection.Descending)
            {
                query = Process.GetProcesses()
               .Where(p => 
               p.ProcessName.ToLower().Contains(_searchString.ToLower().Trim()) || 
               p.MainWindowTitle.ToLower().Contains(_searchString.ToLower().Trim()) ||
               p.Id.ToString().StartsWith(_searchString.ToLower())
               )
               .OrderByDescending(p => prop.GetValue(p, null));
            }
            else
            {
                query = Process.GetProcesses()
               .Where(p => p.ProcessName.ToLower().Contains(_searchString.ToLower()))
               .OrderBy(p => prop.GetValue(p, null));
            }

            processes = query.ToList<Process>();


        }


        public void UpdateSort(string column)
        {
            if (column != _sortColumn)
            {
                _lastSortDirection = ListSortDirection.Descending;
            }
            
            if (_lastSortDirection == ListSortDirection.Descending)
            {
                _lastSortDirection = ListSortDirection.Ascending;
            }
            else
            {
                _lastSortDirection = ListSortDirection.Descending;
            }

            sortColumn = column;
            UpdateProcesses();
        }






        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
