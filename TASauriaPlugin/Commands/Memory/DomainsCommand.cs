namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using System.Linq;
using BizHawk.Client.Common;


public class DomainDescriptor {
    public string Name { get; set; } = "";
    public long Size { get; set; }
}

public class DomainsOutput {
    public string Current { get; set; } = "";
    public DomainDescriptor[] Domains { get; set; } = [];
}

public class DomainsCommand : EmulatorCommand<NoArguments, DomainsOutput>
{
    public DomainsCommand():
        base(
            @"^/memory/domains$"
        )
    {}

    public override DomainsOutput RunSync(ApiContainer api, Dictionary<string, string> arguments, NoArguments payload)
    {
        var domainList = api.Memory.GetMemoryDomainList();

        return new DomainsOutput {
            Current = api.Memory.GetCurrentMemoryDomain(),
            Domains = [.. domainList.Select(name => new DomainDescriptor {
                Name = name,
                Size = api.Memory.GetMemoryDomainSize(name)
            })]
        };
    }
}
