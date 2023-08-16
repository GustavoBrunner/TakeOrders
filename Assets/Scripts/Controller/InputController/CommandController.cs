using System;

namespace Controller.Commands
{
    public class CommandController  
    {
        BaseCommand interactCommand;
        BaseCommand walkCommand;
        public CommandController(BaseCommand interactCommand, BaseCommand walkCommand)
        {
            this.interactCommand = interactCommand;
            this.walkCommand = walkCommand;
        }

        public void Interact()
        {
            this.interactCommand?.Execute();
        }
        public void Walk()
        {
            this.walkCommand?.Execute();
        }
    }
}