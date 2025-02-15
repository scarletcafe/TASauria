namespace ScarletCafe.TASauriaPlugin.Commands.Meta;

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

public class BatchInput {
    public JObject[] Commands { get; set; } = [];
}

public class BatchOutput {
    public JObject[] Output { get; set; } = [];
}

public class BatchCommand : EmulatorCommand<BatchInput, JObject>
{
    public BatchCommand():
        base(
            @"^/meta/batch$"
        )
    {}

    public override JObject RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, BatchInput payload)
    {
        // Get command, argument, and input pairs
        (ICommand, Dictionary<string, string>, JObject)?[] maybePairs = [..
            payload.Commands.Select(new Func<JObject, (ICommand, Dictionary<string, string>, JObject)?>((input) => {
                string? path = input.GetValue("command")?.ToObject<string>();

                if (path == null) {
                    return null;
                }

                var (command, arguments) = Registry.commands
                    .Select(x => (x, x.TestPath(path)))
                    .FirstOrDefault(x => x.Item2 != null);

                if (command == null) {
                    return null;
                }

                return (command, arguments!, input);
            }))
        ];

        // Get the indices of any commands we couldn't find
        int[] missing = [.. maybePairs
            .Select((value, index) => (value, index))
            .Where((pair) => pair.value == null)
            .Select((pair) => pair.index)];

        if (missing.Length > 0) {
            return new JObject {
                { "status", 400 },
                { "error", "Could not resolve commands at indices: " + string.Join(", ", missing) }
            };
        }

        // Run security checks and aggregate any failures
        (int, string)[] securityCheckFailures = [.. maybePairs
            .Select((value, index) => {
                var (command, arguments, input) = value!.Value;

                if (!command.SecurityCheck(arguments, input)) {
                    return (index, command.SecurityRemarks);
                }

                return ((int, string)?)null;
            })
            .Where((value) => value != null)
            .Select((value) => value!.Value)
        ];

        if (securityCheckFailures.Length > 0) {
            return new JObject {
                { "status", 403 },
                { "error", "Some commands failed security checks:\n" + string.Join("\n", securityCheckFailures.Select((value) => $"{value.Item1}: {value.Item2}")) }
            };
        }

        // If we've reached this point, we've resolved all commands and they are all executable.
        JObject[] outputs = [.. maybePairs.Select((value) => {
            var (command, arguments, input) = value!.Value;

            if (command is IEmulatorCommand emulatorCommand) {
                return emulatorCommand.ExecuteSync(emulator, arguments, input);
            } else {
                return command.Execute(arguments, input);
            }
        })];

        return JObject.FromObject(new BatchOutput {
            Output = outputs
        }, jsonSerializer);
    }
}
