using Game.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class DirectionButton : MonoBehaviour, IPointerDownHandler
    {
        public ButtonDirection direction;

        HUD hud;

        void Awake()
        {
            hud = transform.parent.GetComponent<HUD>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            hud.Player.Move(direction);
        }
    }
}