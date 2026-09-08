using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BDVM.Common;

[DataContract]
public sealed class BdvmModulePayload
{
    [DataMember(Name = "moduleId", Order = 1)] public string ModuleId { get; set; } = "";
    [DataMember(Name = "schema", Order = 2)] public string Schema { get; set; } = "";
    [DataMember(Name = "schemaVersion", Order = 3)] public int SchemaVersion { get; set; }
    [DataMember(Name = "payload", Order = 4)] public string Payload { get; set; } = "";
}

[DataContract]
public sealed class BdvmCheckpointEnvelope
{
    public const string CurrentSchema = "bdvm.checkpoint";
    public const int CurrentVersion = 2;

    [DataMember(Name = "schema", Order = 1)] public string Schema { get; set; } = CurrentSchema;
    [DataMember(Name = "schemaVersion", Order = 2)] public int SchemaVersion { get; set; } = CurrentVersion;
    [DataMember(Name = "checkpointId", Order = 3)] public string CheckpointId { get; set; } = "";
    [DataMember(Name = "modules", Order = 4)] public List<BdvmModulePayload> Modules { get; set; } = new List<BdvmModulePayload>();
}
