namespace g
{
    public abstract class ParseArgsResult
    {
        public string[] Arguments { get; set; }
        public ActionTypes ActionType { get; set; }
        public abstract void Execute();
    }
}
