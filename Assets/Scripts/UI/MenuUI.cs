using UnityEngine;
using System;
using UnityEngine.UI;

namespace Game.UI
{
    public class MenuUI : MonoBehaviour
    {
        public event Action PlayPressed;

        [SerializeField] Button playButton;

        void Start()
        {
            playButton.onClick.AddListener(OnPlayButtonPressed);

            void OnPlayButtonPressed() => PlayPressed?.Invoke();
        }
    }
}