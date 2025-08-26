using Infinity.Runtime.PhantomForge.Core;

namespace Infinity.Runtime.PhantomForge.FiringMode
{
    public class ShotgunMode : IFiringMode
    {
        private Weapon _weapon;

        public ShotgunMode(Weapon weapon)
        {
            _weapon = weapon;
        }
        public void Fire()
        {
            throw new System.NotImplementedException();
        }
    }
}