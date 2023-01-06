using MarblesTD.Core.MapSystems;

namespace MarblesTD.Core.Common.Requests.List
{
    public readonly struct PauseScenarioRequest : IRequest<bool>
    {
        public readonly Scenario Scenario;

        public PauseScenarioRequest(Scenario scenario)
        {
            Scenario = scenario;
        }
    }
}