namespace ScarletCafe.TASauriaPlugin.Commands;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

using BizHawk.Client.Common;
using Newtonsoft.Json;

public interface IEmulatorCommand : ICommand {
    /// <summary>
    /// Attempts to execute the command with the given payload.
    /// This is SYNC and should only be run from the emulator main thread.
    /// </summary>
    /// <param name="arguments">Any arguments parsed from the command path</param>
    /// <param name="input">The payload content, as a generic parsed JObject</param>
    /// <returns>The response from the command, as a generic JObject</returns>
    public JObject ExecuteSync(ApiContainer api, Dictionary<string, string> arguments, JObject input);
}

public abstract class EmulatorCommand<Input, Output>
    : Command<Input, Output>, IEmulatorCommand
    where Input: class
    where Output: class
{

    public EmulatorCommand(Regex pattern) : base(pattern) {}
    public EmulatorCommand(string pattern) : base(pattern) {}

    private class ResponseSignal {
        public EventWaitHandle successHandle = new(false, EventResetMode.ManualReset);
        public Output? response = null;
        public EventWaitHandle failureHandle = new(false, EventResetMode.ManualReset);
        public Exception? exception = null;
    }

    public override Output Run(Dictionary<string, string> arguments, Input payload) {
        ResponseSignal signal = new();

        GlobalState.generalUpdateQueue.Enqueue((ApiContainer container) => {
            try {
                Output output = RunSync(container, arguments, payload);
                signal.response = output;
                signal.successHandle.Set();
            } catch (Exception exception) {
                signal.exception = exception;
                signal.failureHandle.Set();
            }
        });

        int index = WaitHandle.WaitAny([signal.successHandle, signal.failureHandle], 10000);

        if (index == WaitHandle.WaitTimeout) {
            throw new TimeoutException("Waited too long for the emulator to respond. Is it frozen or locked up?");
        } else if (index == 0) {
            return signal.response!;
        } else {
            throw signal.exception!;
        }
    }

    public abstract Output RunSync(ApiContainer api, Dictionary<string, string> arguments, Input payload);

    public JObject ExecuteSync(ApiContainer api, Dictionary<string, string> arguments, JObject input)
    {
        // TODO: This code is pretty much identical to Execute, can we do something clean to not duplicate the code here?

        // Convert input to type
        Input? convertedInput = null;
        try {
            convertedInput = input.ToObject<Input>(jsonSerializer);
        } catch (JsonSerializationException) {
            // Missing fields or fields of wrong type
            // We leave convertedInput as null and it gets handled below
        }

        if (convertedInput != null) {
            try {
                Output output = RunSync(api, arguments, convertedInput!);

                if (output != null) {
                    // Convert output back to JObject
                    JObject convertedOutput = JObject.FromObject(output, jsonSerializer);
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
