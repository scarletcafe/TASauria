namespace ScarletCafe.TASauriaPlugin.Commands;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Registry {
    // Automatically scan for and instantiate all the command implementations
    public static readonly List<ICommand> commands =
        [.. AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(selector => selector.GetTypes())
        .Where(predicate =>
            typeof(ICommand).IsAssignableFrom(predicate)
            && !predicate.IsAbstract
        )
        .Select(predicate => predicate.GetConstructor([]))
        .Where(constructor => constructor != null)
        .Select(constructor => (ICommand)constructor.Invoke([]))];

    public static JObject Resolve(string path, JObject input) {
        // Try to find a command that matches the path.
        var (command, arguments) = commands
            .Select(x => (x, x.TestPath(path)))
            .FirstOrDefault(x => x.Item2 != null);

        JObject output;

        if (command != null) {
            output = command.Execute(arguments!, input);
        } else {
            output = new JObject
            {
                { "status", 404 },
                { "error", "No command matched this path." }
            };
        }

        output.Add("messageIdentifier", input.GetValue("messageIdentifier"));

        return output;
    }
}
