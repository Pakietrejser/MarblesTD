using System;
using System.Linq;

namespace MarblesTD.Core.Common.Extensions
{
    public class WeightedChance<T>
    {
        readonly (T, float)[] _weights;
        
        public WeightedChance((T, float)[] weights)
        {
            _weights = weights;
        }

        public T Random()
        {
            float ratioSum = _weights.Sum(p => p.Item2);
            float randomValue = Seed.Random(ratioSum);

            foreach ((var item, float ratio) in _weights)
            {
                randomValue -= ratio;

                if (!(randomValue <= 0))
                    continue;
                
                return item;
            }
            
            throw new ArgumentException("Weights are setup incorrectly.");
        }
    }
}