using UnityEngine;

namespace Game.Player.Ship
{
    public class ModuleHandler : MonoBehaviour
    {
        public HullModuleBase CurrentHull => _currentHull;
        public GunModuleBase CurrentGun => _currentGun;
        public BridgeModuleBase CurrentBridge => _currentBridge;

        private HullModuleBase _currentHull;
        private GunModuleBase _currentGun;
        private BridgeModuleBase _currentBridge;

        public void SetGun(ModuleCreator creator, GunModuleBase gun)
        {
            if(creator == null)
            {
                Debug.LogError("Creator is null");
                return;
            }

            _currentGun = gun;
        }

        public void SetHull(ModuleCreator creator, HullModuleBase hull)
        {
            if (creator == null)
            {
                Debug.LogError("Creator is null");
                return;
            }

            _currentHull = hull;
        }

        public void SetBridge(ModuleCreator creator, BridgeModuleBase viewfinder)
        {
            if (creator == null)
            {
                Debug.LogError("Creator is null");
                return;
            }

            _currentBridge = viewfinder;
        }

    }
}