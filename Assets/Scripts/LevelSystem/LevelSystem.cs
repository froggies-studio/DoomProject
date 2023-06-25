using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSystem
{
    public class LevelSystem : MonoBehaviour
    {
        [SerializeField] private LevelInfo[] levelInfos;

        private int _currentLevelIndex;

        public int MaxLevel => levelInfos.Length;
        
        public int CurrentLevelIndex => _currentLevelIndex;
        
        public event Action<int> OnLevelCleared;
        public event Action<int, LevelInfo> OnLevelStarted;
        public event Action OnGameEnd;

        public void StartFirstLevel()
        {
            OnLevelStarted?.Invoke(_currentLevelIndex, levelInfos[_currentLevelIndex]);
        }
        
        public bool GoToNextLevel()
        {
            if (_currentLevelIndex >= MaxLevel - 1)
            {
                OnGameEnd?.Invoke();
                return false;
            }
            
            _currentLevelIndex++;
            OnLevelStarted?.Invoke(_currentLevelIndex, levelInfos[_currentLevelIndex]);
            return true;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}