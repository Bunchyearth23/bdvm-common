using System;
using System.Collections.Generic;

namespace BDVM.Common;

public sealed class BdvmApiVersion : IComparable<BdvmApiVersion>, IEquatable<BdvmApiVersion>
{
    public BdvmApiVersion(int major, int minor)
    {
        if (major < 0 || minor < 0) throw new ArgumentOutOfRangeException(nameof(major));
        Major = major;
        Minor = minor;
    }

    public int Major { get; }
    public int Minor { get; }
    public int CompareTo(BdvmApiVersion? other) => other == null ? 1 : Major != other.Major ? Major.CompareTo(other.Major) : Minor.CompareTo(other.Minor);
    public bool Equals(BdvmApiVersion? other) => other != null && Major == other.Major && Minor == other.Minor;
    public override bool Equals(object? obj) => Equals(obj as BdvmApiVersion);
    public override int GetHashCode() => (Major * 397) ^ Minor;
    public override string ToString() => Major + "." + Minor;
}

public sealed class BdvmApiRange
{
    public BdvmApiRange(BdvmApiVersion minimum, BdvmApiVersion maximum)
    {
        Minimum = minimum ?? throw new ArgumentNullException(nameof(minimum));
        Maximum = maximum ?? throw new ArgumentNullException(nameof(maximum));
        if (Minimum.CompareTo(Maximum) > 0) throw new ArgumentException("Minimum API version must not exceed maximum.");
    }

    public BdvmApiVersion Minimum { get; }
    public BdvmApiVersion Maximum { get; }
    public bool Supports(BdvmApiVersion version) => version != null && Minimum.CompareTo(version) <= 0 && Maximum.CompareTo(version) >= 0;
}

public sealed class BdvmWebModuleManifest
{
    public string Id { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string ModuleVersion { get; set; } = "";
    public BdvmApiRange RequiredWebApi { get; set; } = new BdvmApiRange(new BdvmApiVersion(1, 0), new BdvmApiVersion(1, 0));
    public string RouteNamespace { get; set; } = "";
    public string AssetNamespace { get; set; } = "";
    public IReadOnlyList<string> Capabilities { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Permissions { get; set; } = Array.Empty<string>();
}

public sealed class BdvmWebRoute
{
    public string Method { get; set; } = "GET";
    public string Path { get; set; } = "";
    public string Permission { get; set; } = "";
    public string IntentType { get; set; } = "";
}

public sealed class BdvmWebNavigationItem
{
    public string Id { get; set; } = "";
    public string Label { get; set; } = "";
    public string Path { get; set; } = "";
    public int Order { get; set; }
}

public sealed class BdvmWebAsset
{
    public string Key { get; set; } = "";
    public string ContentType { get; set; } = "";
}

public sealed class BdvmRealtimeSubscription
{
    public string Topic { get; set; } = "";
    public string Permission { get; set; } = "";
}

public interface IBdvmWebRegistrar
{
    void AddRoute(BdvmWebRoute route);
    void AddNavigation(BdvmWebNavigationItem item);
    void AddAsset(BdvmWebAsset asset);
    void AddSubscription(BdvmRealtimeSubscription subscription);
}

public interface IBdvmWebModule
{
    BdvmWebModuleManifest Manifest { get; }
    void Register(IBdvmWebRegistrar registrar);
}

public sealed class BdvmCapability
{
    public string Id { get; set; } = "";
    public string OwnerModuleId { get; set; } = "";
    public BdvmApiVersion Version { get; set; } = new BdvmApiVersion(1, 0);
}

public interface IBdvmCapabilityRegistry
{
    void Register(BdvmCapability capability);
    bool TryGet(string capabilityId, out BdvmCapability capability);
}
