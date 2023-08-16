using System;
namespace Controller.Commands
{
    public abstract class BaseCommand
    {
        protected Action Action;
        protected string Name;
        public BaseCommand(Action action, string name)
        {
            this.Action = action;
            this.Name = name;
        }
        public abstract void Execute();
    }
}
