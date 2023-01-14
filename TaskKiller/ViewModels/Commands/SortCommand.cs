using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskKiller.ViewModels.Commands
{
    public class SortCommand : ICommand
    {
        public ProcessesVM VM { get; set; }

        public SortCommand(ProcessesVM vm) {
            VM = vm;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (VM.processes.Count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute(object? parameter)
        {
            Debug.WriteLine($"SORT {parameter.ToString()}");
            VM.UpdateSort(parameter.ToString());
        }
    }
}
