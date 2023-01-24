using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles.Modifiers;

namespace MarblesTD.Core.Entities.Marbles
{
    public abstract class Wave
    {
        public abstract int HoneyReward { get; }
        public abstract IEnumerable<WaveGroup> GetGroups();
    }

    public class Wave1 : Wave
    {
        public override int HoneyReward => 50;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 20);
        }
    }
    
    public class Wave2 : Wave
    {
        public override int HoneyReward => 55;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 35);
        }
    }
    
    public class Wave3 : Wave
    {
        public override int HoneyReward => 60;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 25);
            yield return new WaveGroup(2, 5);
        }
    }
    
    public class Wave4 : Wave
    {
        public override int HoneyReward => 65;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 35);
            yield return new WaveGroup(2, 15);
        }
    }
    
    public class Wave5 : Wave
    {
        public override int HoneyReward => 70;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 5);
            yield return new WaveGroup(2, 30);
        }
    }
    
    public class Wave6 : Wave
    {
        public override int HoneyReward => 75;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(1, 15);
            yield return new WaveGroup(2, 15);
            yield return new WaveGroup(3, 5);
        }
    }
    
    public class Wave7 : Wave
    {
        public override int HoneyReward => 80;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(3, 15);
            yield return new WaveGroup(2, 15);
            yield return new WaveGroup(1, 15);
        }
    }
    
    public class Wave8 : Wave
    {
        public override int HoneyReward => 85;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 1, new XL());
        }
    }
    
    public class Wave9 : Wave
    {
        public override int HoneyReward => 90;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 10);
        }
    }
    
    public class Wave10 : Wave
    {
        public override int HoneyReward => 95;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 20);
        }
    }
    
    public class Wave11 : Wave
    {
        public override int HoneyReward => 100;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 10, new Armored());
        }
    }
    
    public class Wave12 : Wave
    {
        public override int HoneyReward => 105;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 5);
            yield return new WaveGroup(10, 1, 2.5f, 1f,  new XL());
            yield return new WaveGroup(10, 5);
            yield return new WaveGroup(10, 1, 2.5f, 1f,  new XL());
        }
    }
    
    public class Wave13 : Wave
    {
        public override int HoneyReward => 110;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 50);
        }
    }
    
    public class Wave14 : Wave
    {
        public override int HoneyReward => 115;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 40, new Armored());
        }
    }
    
    public class Wave15 : Wave
    {
        public override int HoneyReward => 120;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 3, 2.5f, 3f,  new XL(), new Armored());
        }
    }

    public class Wave16 : Wave
    {
        public override int HoneyReward => 125;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 60, new Faster());
        }
    }

    public class Wave17 : Wave
    {
        public override int HoneyReward => 130;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 6, 2.5f, 1f,  new XL(), new Armored());
        }
    }

    public class Wave18 : Wave
    {
        public override int HoneyReward => 135;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 10, new Armored());
            yield return new WaveGroup(10, 2, 2.5f, 1f,  new XL(), new Armored());
            yield return new WaveGroup(10, 10, new Armored());
            yield return new WaveGroup(10, 2, 2.5f, 1f,  new XL(), new Armored());
        }
    }

    public class Wave19 : Wave
    {
        public override int HoneyReward => 140;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 1,  new XXL());
        }
    }

    public class Wave20 : Wave
    {
        public override int HoneyReward => 145;
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(10, 2, 2.5f, 5f,  new XXL());
        }
    }

    public class ModularWave : Wave
    {
        public override int HoneyReward => 45 + _waveIndex * 5;
        readonly int _waveIndex;

        public ModularWave(int waveIndex)
        {
            _waveIndex = waveIndex;
        }
        
        public override IEnumerable<WaveGroup> GetGroups()
        {
            yield return new WaveGroup(2 * _waveIndex, 3 * _waveIndex);
        }
    }
}