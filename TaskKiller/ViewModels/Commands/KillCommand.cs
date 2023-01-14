using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskKiller.ViewModels.Commands
{
    public class KillCommand : ICommand
    {
        public ProcessVM VM { get; set; }
        public event EventHandler? CanExecuteChanged;

        public KillCommand(ProcessVM vm) 
        { 
            VM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            if (VM.process == null)
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.KillProcess();
        }

    }
}
