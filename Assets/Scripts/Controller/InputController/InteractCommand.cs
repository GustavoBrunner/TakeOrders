using System;
namespace Controller.Commands
{
    public class InteractCommand : BaseCommand
    {
        public InteractCommand(Action action, string name) : base(action, name){}

        public override void Execute()
        {
            this.Action.Invoke();
        }
    }
}