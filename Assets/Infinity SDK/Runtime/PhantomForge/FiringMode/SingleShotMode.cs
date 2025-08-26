using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.PhantomForge.Core;
using Infinity.Runtime.Utils;
using UnityEngine;

namespace Infinity.Runtime.PhantomForge.FiringMode
{
    public class SingleShotMode : IFiringMode
    {
        private Weapon _weapon;
        
        public SingleShotMode(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Fire()
        {
            InfinityLog.Info(typeof(SingleShotMode), $"Fire");
        }
    }
}