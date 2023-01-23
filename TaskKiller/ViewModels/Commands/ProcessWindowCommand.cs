using System;
using System.Diagnostics;
using System.Windows.Input;
using TaskKiller.Views;

namespace TaskKiller.ViewModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessWindowCommand : ICommand
    {

        public ProcessesVM VM { get; set; }

        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        public ProcessWindowCommand(ProcessesVM vm)
        {
            VM = vm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            try
            {
                Process? p = (Process?)parameter;
                var test = p.PriorityClass;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            var processWindow = new KillWindow((Process?)parameter);
            processWindow.Show();
        }
    }
}
