using UnityEngine;

namespace Infinity.Runtime.PhantomForge.Core
{
    [CreateAssetMenu(menuName = "Infinity/PhantomForge/New Ammo", fileName = "New Ammo")]
    public class AmmoDescriptor : ScriptableObject
    {
        public string ammoName;
        [TextArea(2, 20)] 
        public string ammoDescription;
        public float damage;
        public float bulletSpeed;
        public AmmoType ammoType;
        
        public GameObject bulletPrefab;
    }
}