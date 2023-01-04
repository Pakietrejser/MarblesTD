using MarblesTD.Core.MapSystems;

namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct MapStartedSignal : ISignal {}

    public readonly struct ScenarioStartedSignal : ISignal
    {
        public readonly Scenario Scenario;

        public ScenarioStartedSignal(Scenario scenario)
        {
            Scenario = scenario;
        }
    }
}