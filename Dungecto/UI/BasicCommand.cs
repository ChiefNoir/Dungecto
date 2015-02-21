using System;
using System.Windows.Input;

namespace Dungecto.UI
{
    /// <summary> Basic input command </summary>
    internal class BasicCommand : ICommand
    {
        /// <summary> Action </summary>
        private Action _action;

        /// <summary> Create basic input command </summary>
        /// <param name="action">Action to execute</param>
        public BasicCommand(Action action)
        {
            _action = action;
        }

        /// <summary>Can execute?</summary>
        /// <param name="parameter">~</param>
        /// <returns> <code>True</code> if command can be executed</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary> Execute command </summary>
        /// <param name="parameter">~</param>
        public void Execute(object parameter)
        {
            _action();
        }
    }
}