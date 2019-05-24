using Game.UI;
using UnityEngine;
using System;

namespace Game.Managers
{
    public class UIManager : MonoBehaviour
    {
        public event Action PlayPressed;
      
        [SerializeField] MenuUI menu;
        [SerializeField] HUD hud;

        static UIManager instance;
        public static UIManager Instance
        {
            get => instance;
            set => instance = instance ?? value;
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            menu.PlayPressed += OnPlayPressed;
            GameManager.Instance.GameEnded += OnGameEnded;

            void OnGameEnded()
            {
                menu.gameObject.SetActive(true);
                HUD.Instance.gameObject.SetActive(false);           
            }

            void OnPlayPressed()
            {
                menu.gameObject.SetActive(false);
                HUD.Instance.gameObject.SetActive(true);

                PlayPressed?.Invoke();
            }
        }
    }
}