using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Player.Ship
{
    public class PlayerModuleCreator : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private PlayerModuleHandler _moduleHandler;

        [SerializeField] private List<PlayerHullModuleBase> _hullPrototypes;
        [SerializeField] private List<PlayerGunModuleBase> _gunPrototypes;

        private PlayerHullModuleBase _currentHullPrototype;
        private PlayerGunModuleBase _currentGunPrototype;

        private void Awake()
        {
            ReferencesCheck();
            Init();
        }

        #region Changing modules

        [ContextMenu("SetNextHull")]
        public void SetNextHull()
        {
            SetNext(_hullPrototypes, ref _currentHullPrototype, false);

            ReplaceHull(_currentHullPrototype);
        }

        [ContextMenu("SetPreviusHull")]
        public void SetPreviusHull()
        {
            SetNext(_hullPrototypes, ref _currentHullPrototype, true);

            ReplaceHull(_currentHullPrototype);
        }

        [ContextMenu("SetNextGun")]
        public void SetNextGun()
        {
            SetNext(_gunPrototypes, ref _currentGunPrototype, false);

            ReplaceGun(_currentGunPrototype);
        }

        [ContextMenu("SetPreviusGun")]
        public void SetPreviusGun()
        {
            SetNext(_gunPrototypes, ref _currentGunPrototype, true);

            ReplaceGun(_currentGunPrototype);
        }

        private void SetNext<T>(List<T> prototypes, ref T currentModule, bool goBack) where T : IModule
        {
            int currentIndex = prototypes.IndexOf(currentModule);
            int targetIndex = currentIndex + (goBack ? -1 : 1);

            if (targetIndex >= prototypes.Count)
            {
                targetIndex = 0;
            }
            else if (targetIndex < 0)
            {
                targetIndex = prototypes.Count - 1;
            }

            currentModule = prototypes[targetIndex];
        }

        #endregion

        private void Init()
        {
            _currentHullPrototype = _hullPrototypes[0];
            _currentGunPrototype = _gunPrototypes[0];

            ReplaceHull(_currentHullPrototype);
            ReplaceGun(_currentGunPrototype);
        }

        private void ReplaceHull(PlayerHullModuleBase hullPrototype)
        {
            if (_moduleHandler.CurrentHull != null)
            {
                Destroy(_moduleHandler.CurrentHull.gameObject);
            }

            PlayerHullModuleBase newHull = hullPrototype.Instatiate(transform, _container);
            _moduleHandler.SetHull(this, newHull);
            ReplaceGun(_currentGunPrototype);
        }

        private void ReplaceGun(PlayerGunModuleBase gunPrototype)
        {
            if (_moduleHandler.CurrentGun != null)
            {
                Destroy(_moduleHandler.CurrentGun.gameObject);
            }

            Transform gunSpot = _moduleHandler.CurrentHull.GunSpot;
            PlayerGunModuleBase newGun = gunPrototype.Instatiate(gunSpot, _container);
            _moduleHandler.SetGun(this, newGun);
        }

        private void ReferencesCheck()
        {
            if (_hullPrototypes.Count == 0)
            {
                Debug.LogError("List of hull prototypes is empty");
            }

            if (_gunPrototypes.Count == 0)
            {
                Debug.LogError("List of gun prototypes is empty");
            }
        }
    }
}