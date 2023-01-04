using MarblesTD.Core.ScenarioSystems;
using PathCreation;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class MarbleControllerView : MonoBehaviour, MarbleController.IView
    {
        public PathCreator[] PathCreators;
        
        public Vector2 GetStartPosition()
        {
            return PathCreators[0].path.GetPointAtTime(0, EndOfPathInstruction.Stop);
        }

        public Vector2 GetEndPosition()
        {
            return PathCreators[0].path.GetPointAtTime(1, EndOfPathInstruction.Stop);
        }

        public Vector2 GetPositionAtDistance(float distance)
        {
            return PathCreators[0].path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        }

        public Quaternion GetRotationAtDistance(float distance)
        {
            return PathCreators[0].path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop);
        }
    }
}