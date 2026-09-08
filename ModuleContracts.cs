using System.Collections.Generic;

namespace BDVM.Common;

public interface IBdvmModule
{
    string Id { get; }
    BdvmApiVersion Version { get; }
    IReadOnlyList<string> Dependencies { get; }
}

public abstract class BdvmModuleBase : IBdvmModule
{
    protected BdvmModuleBase(string id, params string[] dependencies)
    {
        Id = id;
        Dependencies = dependencies;
    }

    public string Id { get; }
    public BdvmApiVersion Version => new BdvmApiVersion(1, 0);
    public IReadOnlyList<string> Dependencies { get; }
}
