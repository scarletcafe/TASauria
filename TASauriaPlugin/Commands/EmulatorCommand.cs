namespace ScarletCafe.TASauriaPlugin.Commands;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using BizHawk.Client.Common;

public abstract class EmulatorCommand<Input, Output> : Command<Input, Output> where Output: class {

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
}
