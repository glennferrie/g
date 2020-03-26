using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Reflection;

namespace g
{
    class Program
    {
        static Dictionary<string, ParseArgsResult> results = new Dictionary<string, ParseArgsResult>();
        static void Main(string[] args)
        {
            // load types into memory
            var patternset = ConfigurationManager.AppSettings["g.addins.searchpattern"];
            var patterns = patternset.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            var basepath = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var searchPattern in patterns)
            {
                var files = Directory.GetFiles(basepath, searchPattern, SearchOption.TopDirectoryOnly);
                foreach(var file in files)
                {
                    Console.WriteLine("Loading actions from " + file);
                    foreach(var result in LoadTypeFromAssembly<ParseArgsResult>(file))
                    {
                        if (string.IsNullOrWhiteSpace(result.SwitchWord))
                        {
                            var typename = result.GetType().FullName;
                            throw new InvalidProgramException(string.Format("This type doesn't have a Switchword, {0}", typename));
                        }
                        results.Add(result.SwitchWord, result);
                    }
                }
            } 
            
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

        private static IEnumerable<TType> LoadTypeFromAssembly<TType>(string file) where TType : class, new()
        {
            Assembly actionsAssembly = Assembly.LoadFile(file);
            var types = actionsAssembly.GetTypes().Where(_type => typeof(ParseArgsResult).IsAssignableFrom(_type));
            foreach(var actionType in types)
            {
                var ctor = actionType.GetConstructor(Type.EmptyTypes);
                yield return ctor.Invoke(null) as TType;
            }
        }

        static ParseArgsResult ParseArgs(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                var lower0 = args[0].ToLower();
                if (results.ContainsKey(lower0))
                {
                    var command = results[lower0];
                    command.Arguments = args;
                    return command;
                }
            }
            return new DoNothingResult(args, ActionTypes.Quit);
        }
    }
}
