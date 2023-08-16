using System;

namespace Controller.Commands
{
    public class WalkCommand : BaseCommand
    {
        public WalkCommand(Action action, string name) : base(action, name){}

        public override void Execute()
        {
            this.Action.Invoke();
        }
    }
}
