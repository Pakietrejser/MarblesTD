using PathCreation;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class ScenarioView : MonoBehaviour
    {
        [SerializeField] PathCreator[] paths;

        public PathCreator[] Paths
        {
            get
            {
                // foreach (var pathCreator in paths)
                // {
                //     int length = pathCreator.path.localPoints.Length;
                //     for (var i = 0; i < length; i++)
                //     {
                //         pathCreator.path.localPoints[i].z = 0;
                //     }
                // }
                
                return paths;
            }
        }
    }
}