using System;
using System.Windows.Input;

namespace TaskKiller.ViewModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class KillCommand : ICommand
    {
        public ProcessVM VM { get; set; }
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        public KillCommand(ProcessVM vm) 
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
            if (VM.process == null)
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
            VM.KillProcess();
        }

    }
}
