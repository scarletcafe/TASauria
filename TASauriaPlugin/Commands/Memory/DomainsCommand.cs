namespace ScarletCafe.TASauriaPlugin.Commands.Memory;

using System.Collections.Generic;
using System.Linq;

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

    public override DomainsOutput RunSync(EmulatorInterface emulator, Dictionary<string, string> arguments, NoArguments payload)
    {
        var domainList = emulator.APIs.Memory.GetMemoryDomainList();

        return new DomainsOutput {
            Current = emulator.APIs.Memory.GetCurrentMemoryDomain(),
            Domains = [.. domainList.Select(name => new DomainDescriptor {
                Name = name,
                Size = emulator.APIs.Memory.GetMemoryDomainSize(name)
            })]
        };
    }
}
