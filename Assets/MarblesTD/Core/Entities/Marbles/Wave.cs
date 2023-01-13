using System.Collections.Generic;

namespace MarblesTD.Core.Entities.Marbles
{
    public abstract class Wave
    {
        public abstract int HoneyReward { get; }
        public abstract IEnumerable<WaveGroup> GetGroups();
    }

    public class Wave1 : Wave
    {
        public override int HoneyReward => 10;

        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 20);
        }
    }
    
    public class Wave2 : Wave
    {
        public override int HoneyReward => 20;
        
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 35);
        }
    }
    
    public class Wave3 : Wave
    {
        public override int HoneyReward => 25;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 25);
            yield return new WaveGroup(2, 5);
        }
    }
    
    public class Wave4 : Wave
    {
        public override int HoneyReward => 30;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 35);
            yield return new WaveGroup(2, 15);
        }
    }
    
    public class Wave5 : Wave
    {
        public override int HoneyReward => 40;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 5);
            yield return new WaveGroup(2, 30);
        }
    }
    
    public class Wave6 : Wave
    {
        public override int HoneyReward => 45;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 15);
            yield return new WaveGroup(2, 15);
            yield return new WaveGroup(3, 5);
        }
    }
    
    public class Wave7 : Wave
    {
        public override int HoneyReward => 60;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(3, 15);
            yield return new WaveGroup(2, 15);
            yield return new WaveGroup(1, 15);
        }
    }
    
    public class Wave8 : Wave
    {
        public override int HoneyReward => 90;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(3, 10);
            yield return new WaveGroup(2, 20);
            yield return new WaveGroup(1, 30);
        }
    }
    
    public class Wave9 : Wave
    {
        public override int HoneyReward => 100;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 5, 3, 2);
        }
    }
    
    public class Wave10 : Wave
    {
        public override int HoneyReward => 100;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 20, 3, 2);
        }
    }
    
    public class Wave11 : Wave
    {
        public override int HoneyReward => 110;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 30, 3, 1.2f);
        }
    }
    
    public class Wave12 : Wave
    {
        public override int HoneyReward => 120;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 40, 3, .8f);
        }
    }
    
    public class Wave13 : Wave
    {
        public override int HoneyReward => 130;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 40);
        }
    }
    
    public class Wave14 : Wave
    {
        public override int HoneyReward => 140;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 60);
        }
    }
    
    public class Wave15 : Wave
    {
        public override int HoneyReward => 150;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 100);
        }
    }

    public class ModularWave : Wave
    {
        public override int HoneyReward => _waveIndex * 10;
        readonly int _waveIndex;

        public ModularWave(int waveIndex)
        {
            _waveIndex = waveIndex;
        }
        
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1 * _waveIndex, 10 * _waveIndex);
        }
    }
}