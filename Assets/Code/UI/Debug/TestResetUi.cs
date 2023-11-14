using Game.Input.System;
using Game.Player.Ship;
using Game.Room;
using Game.Room.Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Player.UI
{
    public class TestResetUi : MonoBehaviour
    {
        [Inject] private TestSceneManager _testSceneManager;
        [Inject] private InputProvider _inputProvider;

        [SerializeField] private Button _onOffButton;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TextMeshProUGUI _currentTimerText;
        [SerializeField] private TextMeshProUGUI _timerListText;
        [SerializeField] private bool _autoLoadRoom = false;

        private float _startRoomTime = 0;
        private List<float> _winTimes = new List<float>();

        private void Start()
        {
            _testSceneManager.Load();

            if (_autoLoadRoom)
            {
                _inputProvider.PlayerControls.Gameplay.Enable();
            }
            else
            {
                OnPanel();
            }
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Update()
        {
            UpdateCurrentRoomTimer();
        }

        private void UpdateCurrentRoomTimer()
        {
            float currentRoomTime = Time.time - _startRoomTime;

            _currentTimerText.text = currentRoomTime.ToString("0.0");
        }

        private void Subscribe()
        {
            _onOffButton.onClick.AddListener(OnOffPanel);
            _restartButton.onClick.AddListener(Restart);
            HullModuleBase.OnDefeatAction += OnDeadPlayer;
            EnemiesManager.OnRoomClear += OnRoomClear;
        }

        private void Unsubscribe()
        {
            _onOffButton.onClick.RemoveListener(OnOffPanel);
            _restartButton.onClick.RemoveListener(Restart);
            HullModuleBase.OnDefeatAction -= OnDeadPlayer;
            EnemiesManager.OnRoomClear -= OnRoomClear;
        }

        private void OnOffPanel()
        {
            if(_panel.activeSelf)
            {
                OffPanel();
            }
            else
            {
                OnPanel();
            }
        }

        private void OffPanel()
        {
            Time.timeScale = 1;
            _inputProvider.PlayerControls.Gameplay.Enable();
            _panel.SetActive(false);
        }

        private void OnPanel()
        {
            Time.timeScale = 0;
            _inputProvider.PlayerControls.Gameplay.Disable();
            _panel.SetActive(true);
        }

        private void Restart()
        {
            _startRoomTime = Time.time;
            _testSceneManager.RestartRoom();
            OffPanel();
            _onOffButton.gameObject.SetActive(true);
            _messageText.text = "hello man";
        }

        private void OnDeadPlayer(HullModuleBase hullModule)
        {
            OnPanel();
            _onOffButton.gameObject.SetActive(false);
            _messageText.text = "u ded :((";
            hullModule.SetStartHP();
        }

        private void OnRoomClear()
        {
            float currentRoomTime = Time.time - _startRoomTime;
            _winTimes.Add(currentRoomTime);
            OnPanel();
            _onOffButton.gameObject.SetActive(false);
            _messageText.text = "wiktory rojale";

            _winTimes.Sort();

            string timesText = "";
            foreach (var time in _winTimes) 
            {
                timesText += time.ToString("0.0") + "\n";
            }

            _timerListText.text = timesText;
        }
    }
}