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
            ActionType = ActionTypes.RunCommand;
        }
        public override void Execute()
        {
            foreach(var path in Arguments.Skip(1)) // skip the first arg
            {
                if (File.Exists(path))
                {
                    Process.Start(new ProcessStartInfo(path));
                }
                else
                {

                    switch(path)
                    {
                        case "-w":
                            Console.WriteLine("Press <Enter> to continue...");
                            var waitFor = Console.ReadLine();
                            break;
                        default:
                            try
                            {
                                Process.Start(new ProcessStartInfo(path));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                }
            }
        }
    }
}
