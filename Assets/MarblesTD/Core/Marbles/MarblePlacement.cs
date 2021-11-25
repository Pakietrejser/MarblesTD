using UnityEngine;

namespace MarblesTD.Core.Marbles
{
    public readonly struct MarblePlacement
    {
        public readonly Marble Marble;
        public readonly Vector2 Position;
        
        public MarblePlacement(Marble marble, Vector2 position)
        {
            Marble = marble;
            Position = position;
        }
    }
}