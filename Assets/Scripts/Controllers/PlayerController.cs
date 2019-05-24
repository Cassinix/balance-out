using Game.Utility;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;

        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(ButtonDirection moveDirection)
        {
            rb.AddForce((moveDirection == ButtonDirection.Up ? Vector3.up : Vector3.down) * 15, ForceMode2D.Impulse);
        }
    }
}
