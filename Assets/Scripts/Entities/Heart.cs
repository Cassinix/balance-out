using UnityEngine;
using System;

namespace Game.Entites
{
    public class Heart : MonoBehaviour
    {
        public event Action Grabbed;
        
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Grabbed?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
