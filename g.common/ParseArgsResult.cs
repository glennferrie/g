using System.Diagnostics;

namespace g
{
    public class ParseArgsResult
    {
        public string SwitchWord { get; set; }
        public string[] Arguments { get; set; }
        public ActionTypes ActionType { get; set; }
        public virtual void Execute()
        {
            Trace.TraceInformation("Base implementation of ParseArgsResult, override me. dont call base.");
        }
    }
}
