namespace MarblesTD.Core.Entities.Marbles
{
    public class WaveProvider
    {
        public int CurrentWave { get; private set; }
        
        public Wave Next()
        {
            CurrentWave++;

            return CurrentWave switch
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
                16 => new Wave16(),
                17 => new Wave17(),
                18 => new Wave18(),
                19 => new Wave19(),
                20 => new Wave20(),
                _ => new ModularWave(CurrentWave)
            };
        }

        public void Reset()
        {
            CurrentWave = 0;
        }
    }
}