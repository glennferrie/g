using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace g
{
    public class RunItResult : ParseArgsResult
    {
        public RunItResult()
        {
            SwitchWord = "run";
            ActionType = ActionTypes.RunCommand;
        }
        public override void Execute()
        {
            var args = Arguments.Skip(1).ToArray();

            // handle simple scenarios
            switch(args.Length)
            {
                case 0:
                    return;
            }

            // handle wait argument
            var wait = false;
            if (args.Contains("-w") || args.Contains("-wait"))
            {
                args = args.TakeWhile(a => a != "-w").ToArray(); // remove the item.
                wait = true;
            }



            if (wait)
            {
                Console.WriteLine("Waiting.... ");
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }
}
