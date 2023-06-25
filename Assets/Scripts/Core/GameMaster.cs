#region

using DebugHelpers;
using EnemySystem;
using StatsSystem;
using UI;
using UnityEngine;

#endregion

namespace Core
{
    public class GameMaster : MonoBehaviour
    {
        [Header("Player systems")]
        [Space(10)]
        [SerializeField] private SanityController sanityController;

        [Header("UI systems")]
        [Space(10)]
        [SerializeField] private SanityUIComponent sanityUI;

        [Header("Level systems")]
        [Space(10)]
        [SerializeField] private LevelSystem.LevelSystem levelSystem;
        
        [Header("Enemy systems")]
        [Space(10)]
        [SerializeField] private EnemySpawnerController enemySpawnerController;
        [SerializeField] private Spawner enemySpawner;
        
        [Header("Debug systems")]
        [Space(10)]
        [SerializeField] private DebugActions debug;
        

        public static GameMaster Instance { get; private set; }


        public DebugActions Debug => debug;
        public SanityController SanityController => sanityController;

        private void Awake()
        { 
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            //Init input system
            var input = InputManager.Instance;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            sanityUI.Init(sanityController);
            
            InitGameLoop();
        }

        private void InitGameLoop()
        {
            if (enemySpawnerController == null || enemySpawner == null || levelSystem == null)
            {
                UnityEngine.Debug.LogWarning("GameMaster: Game loops are not loaded. Game loop systems are not set!");
                return;
            }
                
            
            enemySpawnerController.Init(enemySpawner);
            levelSystem.OnLevelStarted += enemySpawnerController.LevelStarted;
            enemySpawnerController.OnAllEnemiesKilled += () => levelSystem.GoToNextLevel();
            
            levelSystem.StartFirstLevel();
        }
    }
}