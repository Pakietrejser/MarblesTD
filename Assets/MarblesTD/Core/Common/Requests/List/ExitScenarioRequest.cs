using MarblesTD.Core.MapSystems;

namespace MarblesTD.Core.Common.Requests.List
{
    public readonly struct ExitScenarioRequest : IRequest<bool>
    {
        public readonly Scenario Scenario;

        public ExitScenarioRequest(Scenario scenario)
        {
            Scenario = scenario;
        }
    }
}