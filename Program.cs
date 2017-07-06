﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace g
{
    class Program
    {
        Dictionary<string, ParseArgsResult> results = new Dictionary<string, ParseArgsResult>();
        static void Main(string[] args)
        {


            var command = ParseArgs(args);
            switch (command.ActionType)
            {
                case ActionTypes.Quit:
                    Console.WriteLine("Quit");
                    command.Execute();
                    return;
                case ActionTypes.RunCommand:
                    Console.WriteLine("Run Command");
                    command.Execute();
                    return;
                default:
                    Console.WriteLine("Execute: " + command.GetType().Name);
                    command.Execute();
                    return;
            }
        }

        static ParseArgsResult ParseArgs(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                var flatArgs = string.Join(" ", args);
                //Console.WriteLine("parsing args: " + flatArgs);
                var lower0 = args[0].ToLower();
                switch (lower0)
                {
                    //case "run":
                    //case "r":
                    //    return new RunItResult(args);
                    //case "k":
                    //case "kill":
                    //    return new KillItResult(args);

                }
            }
            return new DoNothingResult(args, ActionTypes.Quit);
        }
    }
}
