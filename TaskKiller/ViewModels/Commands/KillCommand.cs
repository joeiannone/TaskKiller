using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskKiller.ViewModel.Commands
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
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.process.Kill();
        }

    }
}
