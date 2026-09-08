# BDVM - Common

`BDVM.Common` contains the small, versioned contracts shared by every BDVM module. It deliberately has no dependency on Unity, Derail Valley, Unity Mod Manager, Multiplayer or any feature module.

## Status

| Property | Value |
| --- | --- |
| Module kind | Shared contracts |
| Target framework | .NET Framework 4.8 (`net48`) |
| Standalone | No; this is a library consumed by other modules |
| Game dependency | None |
| Public API line | BDVM API 1.x |

The project builds independently, but installing its DLL alone does not add gameplay or an interface.

## Responsibilities

- Define module identity, dependency declarations and lifecycle through `IBdvmModule` and `BdvmModuleBase`.
- Version the web extension surface with `BdvmApiVersion` and `BdvmApiRange`.
- Describe web routes, navigation entries, assets, realtime subscriptions and module manifests.
- Expose `IBdvmWebRegistrar` and `IBdvmWebModule` without tying feature modules to a particular HTTP server.
- Publish discoverable capabilities through `BdvmCapability` and `IBdvmCapabilityRegistry`.
- Carry per-module checkpoint payloads in `BdvmCheckpointEnvelope`.
- Provide `IBdvmCompetingGeneratorControl` for optional integrations that can suspend competing world generators.

## Boundaries

This module contains contracts only. It does not start modules, host HTTP routes, own saves, mutate the economy, identify players or call game APIs. Implementations belong to `BDVM.Core`, `BDVM.Web`, feature modules or the final runtime composition.

## Dependencies

There are no project or runtime dependencies beyond `net48`. Consumers should reference the same compatible major API line and must not ship private incompatible copies of these types.

External dependencies: none. Unity, Derail Valley and third-party mod types are forbidden from this contract assembly.

## Build

```powershell
dotnet build .\BDVM.Common.csproj -c Release
```

When building the complete source tree, keep every `BDVM.*` repository as a sibling under `src/` and build `BDVM.slnx` from the workspace root.

## Testing and installation

The repository is covered by the BDVM migration and domain validation suites in the integration workspace. There is no standalone game installation for this module today; `BDVM.Full` is the current installable composition and includes this assembly as a dependency.

## Compatibility and versioning

Additive contract changes stay within the current major API when existing consumers remain source- and behavior-compatible. Breaking contract changes require a new major API line. Web modules declare the API range they accept so the host can refuse incompatible modules before registration.

## License

Licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE).
