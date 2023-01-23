using System;
using System.Collections.Generic;
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
        
        public Dictionary<T, int> Random(int amount)
        {
            int weightsLength = _weights.Length;
            var result = new Dictionary<T, int>(); 
            
            for (var i = 0; i < weightsLength; i++)
            {
                result.Add(_weights[i].Item1, 0);
            }

            for (var i = 0; i < amount; i++)
            {
                var random = Random();
                result[random]++;
            }

            return result;
        }

        public IEnumerable<T> RandomNonRepeating(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount can't be a negative number, or 0");
            
            var weights = new List<(T, float)>(_weights);
            while (amount > 0)
            {
                if (weights.Count == 0) weights = new List<(T, float)>(_weights);
                
                float ratioSum = weights.Sum(p => p.Item2);
                float randomValue = Seed.Random(ratioSum);

                foreach (var weight in weights)
                {
                    randomValue -= weight.Item2;

                    if (!(randomValue <= 0))
                        continue;

                    weights.Remove(weight);
                    yield return weight.Item1;
                    break;
                }

                amount--;
            }

            if (weights.Count == _weights.Length)
            {
                throw new ArgumentException("Weights are setup incorrectly");
            }
        }
    }
}