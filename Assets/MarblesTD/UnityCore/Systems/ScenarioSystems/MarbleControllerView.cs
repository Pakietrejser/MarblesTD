using MarblesTD.Core.ScenarioSystems;
using PathCreation;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class MarbleControllerView : MonoBehaviour, MarbleController.IView
    {
        [SerializeField] PathCreator pathCreator;

        public Vector2 GetStartPosition()
        {
            return pathCreator.path.GetPointAtTime(0, EndOfPathInstruction.Stop);
        }

        public Vector2 GetEndPosition()
        {
            return pathCreator.path.GetPointAtTime(1, EndOfPathInstruction.Stop);
        }

        public Vector2 GetPositionAtDistance(float distance)
        {
            return pathCreator.path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        }

        public Quaternion GetRotationAtDistance(float distance)
        {
            return pathCreator.path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop);
        }
    }
}