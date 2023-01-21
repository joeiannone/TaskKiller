using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskKiller.Views;

namespace TaskKiller.ViewModels.Commands
{
    public class ProcessWindowCommand : ICommand
    {

        public ProcessesVM VM { get; set; }

        public event EventHandler? CanExecuteChanged;

        public ProcessWindowCommand(ProcessesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            try
            {
                Process p = (Process)parameter;
                var test = p.PriorityClass;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            var processWindow = new KillWindow((Process)parameter);
            processWindow.Show();
        }
    }
}
