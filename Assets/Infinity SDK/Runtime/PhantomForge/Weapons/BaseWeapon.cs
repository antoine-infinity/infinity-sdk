namespace Infinity.Runtime.PhantomForge.Weapons
{
    public class BaseWeapon
    {
        public string Name;
        public WeaponSpecs Specs;
    }
    
    public struct WeaponSpecs
    {
        public float FireRate;
        public float Range;
        public float BaseDamage;
        public int ClipSize;
        public float ReloadTime;
        public GrabMode GrabMode;
    }

    public struct AmmoSpecs
    {
        
    }
    
    public enum GrabMode
    {
        OneHanded,
        TwoHanded
    }
}