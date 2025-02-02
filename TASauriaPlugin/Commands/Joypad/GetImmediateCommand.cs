namespace ScarletCafe.TASauriaPlugin.Commands.Joypad;

using System.Collections.Generic;
using System.Linq;
using BizHawk.Client.Common;
using Newtonsoft.Json.Linq;

public class GetImmediateCommand : EmulatorCommand<GetInput, GetOutput>
{
    public GetImmediateCommand():
        base(
            @"/joypad/getimmediate"
        )
    {}

    public override GetOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, GetInput payload)
    {
        var dictionary = api.Joypad.GetImmediate(payload.Controller);

        return new GetOutput {
            State = dictionary.ToDictionary(
                x => x.Key,
                x => {
                    if (x.Value is int v1)
                        return JToken.FromObject(v1);
                    else if (x.Value is bool v2)
                        return JToken.FromObject(v2);
                    return JValue.CreateNull();
                }
            )
        };
    }
}
