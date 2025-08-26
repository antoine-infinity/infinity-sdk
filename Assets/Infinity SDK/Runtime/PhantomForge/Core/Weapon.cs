using Infinity.Runtime.PhantomForge.FiringMode;
using Infinity.Runtime.Utils;
using UnityEngine;

namespace Infinity.Runtime.PhantomForge.Core
{
    public class Weapon : MonoBehaviour
    {
        public WeaponDescriptor descriptor;

        private IFiringMode _firingMode;
        private int _currentAmmo;

        private float _fireTimeout;

        private void Start()
        {
            SetupWeapon();
        }

        private void Update()
        {
            #region DEBUG

            if (1 / descriptor.fireRate < _fireTimeout)
            {
                Fire();
                _fireTimeout = 0;
            }
            else
            {
                _fireTimeout += Time.deltaTime;
            }

            #endregion
        }

        private void SetupWeapon()
        {
            _firingMode = FiringModeFactory.Create(descriptor.fireMode, this);
            _currentAmmo = descriptor.clipSize;
        }

        public void Fire()
        {
            if (_currentAmmo <= 0) return;

            _firingMode.Fire();
            _currentAmmo--;
        }
    }
}