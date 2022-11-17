using MarblesTD.Core.Common.Systems;

namespace MarblesTD.Core.Entities.Marbles
{
    public class MarbleController : GameSystem
    {
        readonly Marble.Factory _marbleFactory;

        static MarbleController Instance;

        public MarbleController(Marble.Factory marbleFactory)
        {
            Instance = this;
            _marbleFactory = marbleFactory;
        }

        public static Marble GetFreshMarbleTest() => Instance._marbleFactory.Create();
    }
}