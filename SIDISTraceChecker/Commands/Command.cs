using System;

namespace SIDISTraceChecker.Commands
{
    public class Command : BaseCommand
    {
        bool _execualbe;
        Action _action;

        public Command(Action action, bool execuable = true)
        {
            _action = action;
            _execualbe = execuable;
        }

        public override bool CanExecute(object parameter)
        {
            return _execualbe;
        }

        public override void Execute(object parameter)
        {
            _action();
        }
    }
}
