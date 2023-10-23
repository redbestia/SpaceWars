using Game.Combat;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Player.Ship
{
    public abstract class PlayerHullModuleBase : PlayerHullBase , IModule
    { 
        [Inject] private DiContainer _container;

        [SerializeField] protected Transform _gunSpot;

        public Transform GunSpot => _gunSpot;

        public abstract bool TryAddUpgrade(IUpgrade upgrade);
        public abstract bool IsUpgradeInstalable(IUpgrade upgrade);

        public PlayerHullModuleBase Instatiate(Transform parent, DiContainer container)
        {
            GameObject hullGM = container.InstantiatePrefab(this, parent);
            PlayerHullModuleBase hull = hullGM.GetComponent<PlayerHullModuleBase>();

            hull.transform.localPosition = transform.localPosition;
            hull.transform.localRotation = transform.localRotation;

            return hull;
        }

        public virtual void AddUpgrade(PlayerHullUpgradeBase upgrade)
        {
            throw new System.NotImplementedException();
        }
    }
}
