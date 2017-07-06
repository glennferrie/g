using System;
using System.Linq;
using System.Diagnostics;

namespace g
{
    public class KillItResult : ParseArgsResult
    {
        public KillItResult(string[] args)
        {
            this.Arguments = args;
            this.ActionType = ActionTypes.KillProcess;
        }
        private bool ArgsContainsAnyVariationFor(string switchName, string delimiters = "-/")
        {
            if (string.IsNullOrWhiteSpace(switchName)) throw new ArgumentNullException(nameof(switchName));
            if (string.IsNullOrWhiteSpace(delimiters)) throw new ArgumentNullException(nameof(delimiters));
            var u = switchName.ToUpper();
            var l = switchName.ToLower();

            for(var q = 0; q < delimiters.Length; q++)
            {
                var d = delimiters.Substring(q, 1);
                if (Arguments.Contains(d + u)) return true;
                if (Arguments.Contains(d + l)) return true;
            }
            return false;
        }

        public override void Execute()
        {
            var force = false;
            var andChildren = false;

            if (ArgsContainsAnyVariationFor("f"))
            {
                force = true;
            }
            if (ArgsContainsAnyVariationFor("c") || ArgsContainsAnyVariationFor("t"))
            {
                andChildren = true;
            }

            var commandArgs = "/IM " + Arguments.Skip(1).Take(1).First();
            if (force)
            {
                commandArgs += " /F";
            }
            if (andChildren)
            {
                commandArgs += " /T";
            }

            try
            {
                Process.Start(new ProcessStartInfo("taskkill", commandArgs) { CreateNoWindow = true, UseShellExecute = true });
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in KillItResult: {0}", ex);
                Console.WriteLine("Press <Enter> to continue...");
                var holdPlease = Console.ReadLine();
            }
        }
    }
}
