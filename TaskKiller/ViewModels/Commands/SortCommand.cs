using System;
using System.Windows.Input;

namespace TaskKiller.ViewModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class SortCommand : ICommand
    {
        public ProcessesVM VM { get; set; }

        public event EventHandler? CanExecuteChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        public SortCommand(ProcessesVM vm) {
            VM = vm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            VM.UpdateSort(parameter.ToString());
        }
    }
}
