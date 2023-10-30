using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Ship
{
    public class DirectionHoldBridge : BridgeModuleBase
    {
        private Quaternion _startAimingGunRot;

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void Update()
        {
            UpdateGunAim();
        }

        private void UpdateMovement()
        {
            if (!IsAiming)
            {
                _playerMovement.RotateToCursor();
            }

            _playerMovement.VerdicalMove();
            _playerMovement.HorizontalMove();
        }

        private void UpdateGunAim()
        {
            if (!IsAiming)
                return;

            AimGun();
        }

        public override void OnStartAim()
        {
            _startAimingGunRot = Gun.transform.localRotation;
            _body.angularVelocity = 0;
        }

        public override void OnEndAim()
        {
            Gun.transform.localRotation = _startAimingGunRot;
        }

        private void AimGun()
        {
            Vector2 mousePos = _Input.CursorPosition.ReadValue<Vector2>();
            Vector2 aimPoint = Utils.ScreanPositionOn2DIntersection(mousePos);

            Vector2 gunPos = (Vector2)Gun.transform.position;
            float angleDegrees = Utils.AngleDirected(gunPos, aimPoint) - 90f;

            Quaternion rotation = Quaternion.Euler(0, 0, angleDegrees);
            Gun.transform.rotation = rotation;
        }
    }
}
