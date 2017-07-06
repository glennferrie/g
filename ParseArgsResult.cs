namespace g
{
    public abstract class ParseArgsResult
    {
        public string[] Arguments { get; protected set; }
        public ActionTypes ActionType { get; protected set; }
        public abstract void Execute();
    }
}
