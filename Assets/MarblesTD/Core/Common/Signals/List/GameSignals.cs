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

    public readonly struct ButtonClickSignal : ISignal {}
    public readonly struct ButtonHoverSignal : ISignal {}
    
    public readonly struct RoundStartedSignal : ISignal {}

    public readonly struct RoundEndedSignal : ISignal
    {
        public readonly int HoneyReward;

        public RoundEndedSignal(int honeyReward)
        {
            HoneyReward = honeyReward;
        }
    }
}