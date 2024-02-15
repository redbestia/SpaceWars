using Game.Player.UI;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Room.Enemy
{
    public class EnemyRoomAlarm : MonoBehaviour
    {
        public bool IsActivated => _isActivated;

        [Inject] private List<EnemyBase> _enemies;
        [Inject] private TestAlarmUI _alarmUi;

        private bool _isActivated = false;

        public void ActivateAlarm()
        {
            if (_isActivated)
            {
                Debug.Log("Alarm is already activated");
                return;
            }

            _isActivated = true;

            foreach (EnemyBase enemy in _enemies)
            {
                if (enemy == null)
                    continue;

                if (enemy.StateMachine.CurrentState is not EnemyGuardStateBase)
                    continue;

                enemy.StateMachine.SwitchToCombatState();
            }

            _alarmUi.Activate();
        }
    }
}
