using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKiller.ViewModels.Commands;

namespace TaskKiller.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessVM : INotifyPropertyChanged
    {

        private Process? _process;
        public event PropertyChangedEventHandler? PropertyChanged;
        public KillCommand KillCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProcessVM() 
        {
            KillCommand = new KillCommand(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public Process? process { 
            get 
            {
                return _process; 
            } 
            set
            {
                _process = value;   
                OnPropertyChanged(nameof(process));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void KillProcess()
        {
            if (process != null)
            {
                process.Kill();   
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
