using Infinity.Runtime.PhantomForge.Core;

namespace Infinity.Runtime.PhantomForge.FiringMode
{
    public class ChargeShotMode : IFiringMode
    {
        private Weapon _weapon;
        public ChargeShotMode(Weapon weapon)
        {
            _weapon = weapon;
        }
        
        public void Fire()
        {
            throw new System.NotImplementedException();
        }
    }
}