using System.Collections.Generic;
using UnityEngine;
 
namespace Game.Player.Upgrade
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/Managment")]
    public class UpgradeOrder : ScriptableObject
    {
        [SerializeField] private List<UpgradeBase> upgrades = new List<UpgradeBase>();

        public List<UpgradeBase> Upgrades => upgrades;
    }
}