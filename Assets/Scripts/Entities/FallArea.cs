using UnityEngine;
using System;
using Game.Utility;

namespace Game.Entites
{
    public class FallArea : MonoBehaviour
    {
        public event Action PlayerFallen;
        public event Action BombFallen;
        public event Action HeartFallen;

        void OnTriggerEnter2D(Collider2D other)
        {
            var isBombFallen = other.gameObject.CompareTag("Bomb");
            var isPlayerFallen = other.gameObject.CompareTag("Player");
            var isHeartFallen = other.gameObject.CompareTag("Heart");

            if (isPlayerFallen)
            {
                PlayerFallen?.Invoke();
            }
            else
            if (isBombFallen)
            {
                BombFallen?.Invoke();
            }
            else
            if (isHeartFallen)
            {
                HeartFallen?.Invoke();
            }
        }
    }
}