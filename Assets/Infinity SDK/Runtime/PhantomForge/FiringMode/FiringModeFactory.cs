using System;
using Infinity.Runtime.PhantomForge.Core;

namespace Infinity.Runtime.PhantomForge.FiringMode
{
    public static class FiringModeFactory
    {
        public static IFiringMode Create(FireModeType type, Weapon weapon)
        {
            switch (type)
            {
                case FireModeType.Single: return new SingleShotMode(weapon);
                case FireModeType.Burst: return new BurstShotMode(weapon);
                case FireModeType.Auto: return new FullAutoMode(weapon);
                case FireModeType.Charge: return new ChargeShotMode(weapon);
                case FireModeType.Shotgun: return new ShotgunMode(weapon);
                default: return new SingleShotMode(weapon);
            }
        }
    }
}