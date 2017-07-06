namespace g
{
    public class DoNothingResult : ParseArgsResult
    {
        public DoNothingResult(string[] args, ActionTypes type)
        {
            ActionType = type;
            Arguments = args;
        }

        public override void Execute()
        {
            // do nothing
        }
    }
}
