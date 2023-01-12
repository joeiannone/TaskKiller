using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKiller.ViewModel.Commands;

namespace TaskKiller.ViewModel
{
    public class ProcessVM : INotifyPropertyChanged
    {

        private Process? _process;
        public event PropertyChangedEventHandler? PropertyChanged;
        public KillCommand KillCommand { get; set; }

        public ProcessVM() 
        {
            KillCommand = new KillCommand(this);
        }

        public Process process { 
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

        

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
