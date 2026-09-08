namespace BDVM.Common;

public interface IBdvmCompetingGeneratorControl
{
    bool IsAvailable { get; }
    bool TryApplyStrictEconomyPolicy(string operationId);
    bool IsStrictEconomyPolicyApplied { get; }
}
