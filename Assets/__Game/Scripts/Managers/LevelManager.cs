﻿using Assets.__Game.Scripts.Enums;
using Assets.__Game.Scripts.EventBus;
using UnityEngine;

namespace Assets.__Game.Scripts.Managers
{
  public class LevelManager : MonoBehaviour
  {
    [SerializeField] private GameObject[] _levelPrefabs;

    private int _overallLevelIndex = 0;
    private int _currentLevelIndex = 0;
    private GameObject _currentLevelPrefab;

    private GameSettings _gameSettings;

    private EventBinding<EventStructs.UiButtonEvent> _uiButtonEvent;

    private void Awake()
    {
      LoadSettings();
    }

    private void OnEnable()
    {
      _uiButtonEvent = new EventBinding<EventStructs.UiButtonEvent>(LoadNextLevel);
    }

    private void OnDisable()
    {
      _uiButtonEvent.Remove(LoadNextLevel);
    }

    private void Start()
    {
      SetSavedLevel();
      LoadLevel(_currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
      if (levelIndex >= _levelPrefabs.Length)
      {
        int randomIndex = Random.Range(1, _levelPrefabs.Length);

        levelIndex = randomIndex;
      }

      if (levelIndex < _levelPrefabs.Length)
      {
        _currentLevelPrefab = _levelPrefabs[levelIndex];

        Instantiate(_currentLevelPrefab);
      }
    }

    private void LoadNextLevel(EventStructs.UiButtonEvent uiButtonEvent)
    {
      if (uiButtonEvent.UiEnums != UiEnums.WinNextLevelButton) return;

      _overallLevelIndex++;
      _gameSettings.OverallLevelIndex = _overallLevelIndex;
      _currentLevelIndex++;
      _gameSettings.LevelIndex = _currentLevelIndex;

      if (_currentLevelIndex >= _levelPrefabs.Length)
        _currentLevelIndex = Random.Range(0, _levelPrefabs.Length);

      SettingsManager.SaveSettings(_gameSettings);
    }

    private void LoadSettings()
    {
      _gameSettings = SettingsManager.LoadSettings<GameSettings>();

      if (_gameSettings == null)
        _gameSettings = new GameSettings();
    }

    private void SetSavedLevel()
    {
      _currentLevelIndex = _gameSettings.LevelIndex;
      _overallLevelIndex = _gameSettings.OverallLevelIndex;
    }
  }
}