using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskKiller.ViewModels.Commands;
using System.Reflection;

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
        private Thread UpdateProcessesThread;
        private Thread SignalAutoUpdateThread;
        private bool _updateProcessesFlag = false;
        private readonly int _autoUpdateSeconds = 5;

        public SortCommand SortCommand { get; set; }
        public ProcessWindowCommand ProcessWindowCommand { get; set; }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public ProcessesVM()
        {   
            // initialize processes
            _processes = new List<Process>();
            _sortColumn = "WorkingSet64";
            _lastSortDirection = ListSortDirection.Descending;

            SortCommand = new SortCommand(this);
            ProcessWindowCommand = new ProcessWindowCommand(this);


            /**
             * Start a new thread to handle requests to update 'processes'
             * signaled with the '_updateProcessesFlag'
             */
            _updateProcessesFlag = true;
            UpdateProcessesThread = new Thread(() =>
            {
                while (true)
                {
                    if (_updateProcessesFlag)
                    {
                        UpdateProcesses();
                        _updateProcessesFlag = false;
                    }
                }
                
            });
            UpdateProcessesThread.Start();


            /**
             * A separate thread to signal to update processes every n (_autoUpdateSeconds) seconds
             */
            SignalAutoUpdateThread = new Thread(async () =>
            {
                PeriodicTimer periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(_autoUpdateSeconds));
                while (await periodicTimer.WaitForNextTickAsync())
                {
                    _updateProcessesFlag = true;
                }
            });
            SignalAutoUpdateThread.Start();

        }

        /// <summary>
        /// 
        /// </summary>
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
        
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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
                _updateProcessesFlag = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateProcesses()
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


            lock (processes)
            {
                processes = query.ToList<Process>();
            }
            
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
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

            _updateProcessesFlag = true;
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
