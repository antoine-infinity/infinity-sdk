using Infinity.Runtime.PhantomForge.Core;

namespace Infinity.Runtime.PhantomForge.FiringMode
{
    public class FullAutoMode : IFiringMode
    {
        private Weapon _weapon;
        public FullAutoMode(Weapon weapon)
        {
            _weapon = weapon;
        }
        
        public void Fire()
        {
            throw new System.NotImplementedException();
        }
    }
}