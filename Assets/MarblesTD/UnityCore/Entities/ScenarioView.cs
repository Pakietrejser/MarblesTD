using PathCreation;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class ScenarioView : MonoBehaviour
    {
        [SerializeField] PathCreator[] paths;
        [SerializeField] float[] distribution;

        public PathCreator[] Paths => paths;
        public float[] PathDistributions => distribution;
    }
}