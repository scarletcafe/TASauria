namespace ScarletCafe.TASauriaPlugin.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

public interface ICommand {

    /// <summary>
    /// Tests whether the path provided matches the path this command is expecting.
    /// </summary>
    /// <param name="path">The path to test, e.g. `/path/here`.</param>
    /// <returns>If the path matches, a dictionary of named matches from the regex. Otherwise, returns null.</returns>
    public Dictionary<string, string>? TestPath(string path);

    /// <summary>
    /// Determines whether this command is allowed to be run with the current security settings.
    /// Commands implement this to predicate their execution on security conditions.
    /// </summary>
    /// <param name="arguments">Any arguments parsed from the command path</param>
    /// <param name="input">The payload content, as a generic parsed JObject</param>
    /// <returns>Whether this command is allowed to run.</returns>
    public bool SecurityCheck(Dictionary<string, string> arguments, JObject input);

    /// <summary>
    /// Any remarks the command has as to its security requirements.
    /// This information will be relayed to the client if the security check fails.
    /// </summary>
    public string SecurityRemarks { get; }

    /// <summary>
    /// Attempts to run the command using the provided payload.
    /// </summary>
    /// <param name="arguments">Any arguments parsed from the command path</param>
    /// <param name="input">The payload content, as a generic parsed JObject</param>
    /// <returns>The response from the command, as a generic JObject</returns>
    public JObject Execute(Dictionary<string, string> arguments, JObject input);
}

public abstract class Command<Input, Output>: ICommand where Input: class {

    protected static readonly JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings {
        ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy(),
        },
        Formatting = Formatting.None,
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        DateParseHandling = DateParseHandling.DateTime,
    });

    private readonly Regex pattern;

    public Command(Regex pattern) {
        this.pattern = pattern;
    }

    public Command(string pattern) {
        this.pattern = new Regex(pattern);
    }

    public abstract Output Run(Dictionary<string, string> arguments, Input payload);

    public virtual bool SecurityCheck(Dictionary<string, string> arguments, JObject input) {
        return true;
    }

    public virtual string SecurityRemarks { get; } = "";

    public Dictionary<string, string>? TestPath(string path)
    {
        Match match = pattern.Match(path);

        if (match.Success) {
            Dictionary<string, string> dictionary = pattern.GetGroupNames().ToDictionary(x => x, x => match.Groups[x].Value);
            return dictionary;
        } else {
            return null;
        }
    }

    public JObject Execute(Dictionary<string, string> arguments, JObject input)
    {
        // Convert input to type
        Input? convertedInput = null;
        try {
            convertedInput = input.ToObject<Input>(jsonSerializer);
        } catch (JsonSerializationException) {
            // Missing fields or fields of wrong type
            // We leave convertedInput as null and it gets handled below
        } catch (JsonReaderException)
        {
            // Rare exception, caused by things like ints over max size
        }

        if (convertedInput != null) {
            try {
                Output output = Run(arguments, convertedInput!);

                if (output != null) {
                    // Convert output back to JObject
                    JObject convertedOutput = JObject.FromObject(output, jsonSerializer);

                    if (!convertedOutput.ContainsKey("status"))
                        convertedOutput.Add("status", 200);

                    return convertedOutput;
                } else {
                    return new JObject() {
                        {"status", 200},
                    };
                }

            } catch (Exception exception) {
                return new JObject() {
                    {"status", 500},
                    {"error", $"Command execution failed with exception:\n{exception}"}
                };
            }
        } else {
            return new JObject() {
                {"status", 400},
                {"error", "The payload was not provided in the correct format."}
            };
        }
    }
}

public class NoArguments {}
