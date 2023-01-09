using MarblesTD.Core.MapSystems;

namespace MarblesTD.Core.Common.Requests.List
{
    public readonly struct ExitScenarioRequest : IRequest<bool>
    {
        public readonly bool PlayerWon;
        public readonly Scenario Scenario;
        public readonly int WavesCompleted;

        public ExitScenarioRequest(Scenario scenario, bool playerWon, int wavesCompleted)
        {
            Scenario = scenario;
            PlayerWon = playerWon;
            WavesCompleted = wavesCompleted;
        }
    }
}