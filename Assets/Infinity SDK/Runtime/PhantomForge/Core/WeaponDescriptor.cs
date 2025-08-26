using UnityEngine;

namespace Infinity.Runtime.PhantomForge.Core
{
    [CreateAssetMenu(menuName = "Infinity/PhantomForge/New Weapon", fileName = "New Weapon")]
    public class WeaponDescriptor : ScriptableObject
    {
        public string weaponName;
        [TextArea(2, 20)]
        public string weaponDescription;
        public GameObject weaponPrefab;

        public AmmoDescriptor ammo;
        public float fireRate;
        public int clipSize;
        public int bulletPoolSize;
        public FireModeType fireMode;

        public AudioClip fireSound;
    }
}