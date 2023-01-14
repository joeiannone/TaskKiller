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
    public class ProcessVM : INotifyPropertyChanged
    {

        private Process? _process;
        public event PropertyChangedEventHandler? PropertyChanged;
        public KillCommand KillCommand { get; set; }

        public ProcessVM() 
        {
            KillCommand = new KillCommand(this);
        }

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


        public event EventHandler ProcessKilled;

        public virtual void OnProcessKilled(object sender, EventArgs e)
        {
            ProcessKilled?.Invoke(this, e);
        }

        public void KillProcess()
        {
            if (process != null)
            {
                //process.
                process.Kill();
                //process.Exited += new EventHandler(OnProcessKilled);
                
            }
            
        }





        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
