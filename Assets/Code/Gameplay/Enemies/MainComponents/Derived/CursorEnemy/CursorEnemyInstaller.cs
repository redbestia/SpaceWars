using Game.Utility;
using UnityEngine.AI;

namespace Game.Room.Enemy
{
    public class CursorEnemyInstaller : EnemyInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();

            Utils.BindGetComponent<NavMeshAgent>(Container);
            Utils.BindGetComponent<EnemyNavMeshAgentRotate>(Container);

            Container.Bind<PatrolController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CursorEnemyGun>().FromComponentInHierarchy().AsSingle();
        }
    }
}
