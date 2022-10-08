using System;
using System.Windows.Input;

namespace MasterAggregator.Desktop.Commands
{
    internal abstract class BaseCommands : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        bool ICommand.CanExecute(object parameter) => CanExecute(parameter);

        void ICommand.Execute(object parameter)
        {
            if (((ICommand)this).CanExecute(parameter))
                Execute(parameter);
        }

        protected virtual bool CanExecute(object p) => true;

        protected abstract void Execute(object p);
    }
}
