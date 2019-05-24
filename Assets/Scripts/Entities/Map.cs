using UnityEngine;
using System;
using Game.Controllers;

namespace Game.Entites
{
    public class Map : MonoBehaviour
    {
        public event Action HeartGrabbed;
        public event Action BombFallen;
        public event Action PlayerFallen;
        public event Action HeartFallen;

        public Heart heart;
        public Rigidbody2D bomb;
        public Rigidbody2D plank;
        public FallArea fallArea;
        public PlayerController player;

        Rigidbody2D playerRB;
        Rigidbody2D heartRB;

        public PlayerController Player { get => player; private set => player = value; }

        void Awake()
        {
            heartRB = heart.GetComponent<Rigidbody2D>();
            playerRB = player.GetComponent<Rigidbody2D>();

            heart.Grabbed += OnHeartGrabbed;
            fallArea.PlayerFallen += OnPlayerFallen;
            fallArea.BombFallen += OnBombFallen;
            fallArea.HeartFallen += OnHeartFallen;
        }
            void OnHeartGrabbed() => HeartGrabbed?.Invoke();
            void OnHeartFallen() => HeartFallen?.Invoke();
            void OnBombFallen() => BombFallen?.Invoke();
            void OnPlayerFallen() => PlayerFallen?.Invoke();
        public void FreezeObjects(bool freeze)
        {
            var constraintsType = freeze ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;

            if (heartRB != null)
            {
                heartRB.constraints = constraintsType;
            }

            playerRB.constraints = constraintsType;
            bomb.constraints = constraintsType;
            plank.constraints = constraintsType;
        }
    }
}
