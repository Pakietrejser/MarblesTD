using MarblesTD.Core.MapSystems;

namespace MarblesTD.Core.Common.Requests.List
{
    public readonly struct StartScenarioRequest : IRequest<bool>
    {
        public readonly Scenario Scenario;

        public StartScenarioRequest(Scenario scenario)
        {
            Scenario = scenario;
        }
    }
}