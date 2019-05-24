using System;
using System.Collections;
using Game.Controllers;
using Game.Managers;
using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class HUD : MonoBehaviour
    {
        public event Action WinTextHided;
        public event Action LevelTextHided;
        public event Action TutorialTextHided;

        public TextMeshProUGUI tutorialText;
        public TextMeshProUGUI winText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI levelEndText;

        static HUD instance;
        public static HUD Instance
        {
            get => instance;
            set => instance = instance ?? value;
        }

        public PlayerController Player { get; private set; }

        WaitForSeconds winTextHideDelay = new WaitForSeconds(1.5f);
        WaitForSeconds levelTextHideDelay = new WaitForSeconds(1);
        WaitForSeconds tutorialTextHideDelay = new WaitForSeconds(2);

        string[] randomLevelEndWords = new string[] { "Good!", "Awesome!", "Perfect!" };

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            GameManager.Instance.MapChanged += OnMapChanged;
            GameManager.Instance.NoMapsRemainng += OnNoMapsRemaining;
            GameManager.Instance.LevelEnded += OnLevelEnded;

            winText.gameObject.SetActive(false);
            gameObject.SetActive(false);

            void OnMapChanged()
            {
                Player = GameManager.Instance.CurrentMap.Player;

                levelText.text = $"Level {GameManager.Instance.MapIndex + 1}";
                levelText.gameObject.SetActive(true);

                StartCoroutine(HideLevelText());
                IEnumerator HideLevelText()
                {
                    yield return levelTextHideDelay;

                    levelText.gameObject.SetActive(false);
                    LevelTextHided?.Invoke();
                }
            }

            void OnNoMapsRemaining()
            {
                winText.gameObject.SetActive(true);

                StartCoroutine(HideWinText());
                IEnumerator HideWinText()
                {
                    yield return winTextHideDelay;

                    winText.gameObject.SetActive(false);
                    WinTextHided?.Invoke();
                }
            }

            void OnLevelEnded()
            {
                var randomWordIndex = UnityEngine.Random.Range(0, randomLevelEndWords.Length);

                levelEndText.text = randomLevelEndWords[randomWordIndex];
                levelEndText.gameObject.SetActive(true);

                StartCoroutine(HideWinText());
                IEnumerator HideWinText()
                {
                    yield return levelTextHideDelay;

                    levelEndText.gameObject.SetActive(false);
                }
            }
        }

        void OnEnable()
        {
            tutorialText.gameObject.SetActive(true);

            StartCoroutine(HideTutorialText());
            IEnumerator HideTutorialText()
            {
                yield return tutorialTextHideDelay;

                tutorialText.gameObject.SetActive(false);
                TutorialTextHided?.Invoke();
            }
        }
    }
}