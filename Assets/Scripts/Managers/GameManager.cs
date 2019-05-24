using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entites;
using Game.UI;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action MapChanged;
        public event Action NoMapsRemainng;
        public event Action GameEnded;
        public event Action LevelEnded;

        public int MapIndex { get; set; }

        [SerializeField] List<GameObject> maps;

        static GameManager instance;
        public static GameManager Instance
        {
            get => instance;
            set => instance = instance ?? value;
        }

        Map currentMap;
        public Map CurrentMap
        {
            get => currentMap;
            set
            {
                currentMap = value;
                MapChanged?.Invoke();
            }
        }

        bool isBombFallen;
        bool isHeartGrabbed;
        bool isGameEnded;

        WaitForSeconds levelLoadDelay = new WaitForSeconds(1);

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            HUD.Instance.WinTextHided += OnWinTextHided;
            HUD.Instance.TutorialTextHided += OnTutorialTextHided;
            UIManager.Instance.PlayPressed += OnPlayPressed;

            void OnTutorialTextHided()
            {
                CurrentMap.FreezeObjects(false);
            }

            void OnWinTextHided()
            {
                Destroy(CurrentMap.gameObject);
                MapIndex = 0;

                GameEnded?.Invoke();
            }

            void OnPlayPressed()
            {
                isGameEnded = false;
                LoadLevel();
                CurrentMap.FreezeObjects(true);
            }

            void LoadLevel()
            {
                if (CurrentMap != null)
                {
                    Destroy(CurrentMap.gameObject);
                }

                CurrentMap = Instantiate(maps[MapIndex]).GetComponent<Map>();
                isBombFallen = false;
                isHeartGrabbed = false;

                CurrentMap.PlayerFallen += OnPlayerFallen;
                CurrentMap.HeartGrabbed += OnHeartGrabbed;
                CurrentMap.HeartFallen += OnHeartFallen;
                CurrentMap.BombFallen += OnBombFallen;

                void OnHeartFallen()
                {
                    if (!isGameEnded)
                    {
                        LoadLevel();
                    }
                }

                void OnBombFallen()
                {
                    isBombFallen = true;

                    if (isHeartGrabbed)
                    {
                        LoadNextLevel();
                    }
                }

                void OnPlayerFallen()
                {
                    if (!isGameEnded)
                    {
                        LoadLevel();
                    }
                }

                void OnHeartGrabbed()
                {
                    isHeartGrabbed = true;

                    if (isBombFallen)
                    {
                        LoadNextLevel();
                    }
                }

                void LoadNextLevel()
                {
                    LevelEnded?.Invoke();

                    StartCoroutine(LoadNewLevel());
                    IEnumerator LoadNewLevel()
                    {
                        CurrentMap.FreezeObjects(true);

                        yield return levelLoadDelay;

                        EndLevel();
                    }
                }

                void EndLevel()
                {
                    if (isGameEnded)
                    {
                        return;
                    }

                    MapIndex++;

                    if (MapIndex < maps.Count)
                    {
                        LoadLevel();
                    }
                    else
                    {
                        isGameEnded = true;
                        NoMapsRemainng?.Invoke();
                    }
                }
            }
        }
    }
}