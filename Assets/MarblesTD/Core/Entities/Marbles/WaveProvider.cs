namespace MarblesTD.Core.Entities.Marbles
{
    public class WaveProvider
    {
        int _currentWave;
        
        public Wave Next()
        {
            _currentWave++;

            return _currentWave switch
            {
                1 => new Wave1(),
                2 => new Wave2(),
                3 => new Wave3(),
                4 => new Wave4(),
                5 => new Wave5(),
                6 => new Wave6(),
                7 => new Wave7(),
                8 => new Wave8(),
                9 => new Wave9(),
                10 => new Wave10(),
                11 => new Wave11(),
                12 => new Wave12(),
                13 => new Wave13(),
                14 => new Wave14(),
                15 => new Wave15(),
                _ => new ModularWave(_currentWave)
            };
        }

        public void Reset()
        {
            _currentWave = 0;
        }
    }
}