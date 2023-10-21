using Game.Combat;
using Game.Player.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Ship
{
    public abstract class PlayerHullBase : ShipPart, IHittable
    {
        public abstract void OnHit();
    }
}