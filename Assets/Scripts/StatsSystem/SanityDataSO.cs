#region

using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace StatsSystem
{
    [CreateAssetMenu(menuName = "Create SanityDataSO", fileName = "SanityDataSO", order = 0)]
    public class SanityDataSO : ScriptableObject
    {
        public float startingDegradationRate;
        public float maxSanity;
        public float startSanity;
        public int stagesCount;
    }

    // public class SanityController : MonoBehaviour, IDamageReceiver
    // {
    //     [SerializeField, Tooltip("Should be in order from biggest to smallest")]
    //     private float[] maxSanityPoints;
    //
    //     [SerializeField] private float degradationRate = 0.1f;
    //     public static SanityController Instance { get; private set; }
    //     private float _currentMaxSanityPoints;
    //
    //     private int _currentSanityLevel;
    //     private float _nextLevelMaxSanityPoints;
    //     private float _sanityPoints;
    //
    //     public int CurrentSanityLevel => _currentSanityLevel;
    //
    //     public float[] MaxSanityPoints => maxSanityPoints;
    //
    //     public float SanityPoints => _sanityPoints;
    //     public event Action OnDead;
    //
    //     private void Awake()
    //     {
    //         Instance = this;
    //     }
    //
    //     private void Start()
    //     {
    //         _currentSanityLevel = 0;
    //         _currentMaxSanityPoints = maxSanityPoints[_currentSanityLevel];
    //         _nextLevelMaxSanityPoints = maxSanityPoints.Length >= 2 ? maxSanityPoints[_currentSanityLevel + 1] : 0;
    //         _sanityPoints = _currentMaxSanityPoints;
    //
    //         //GameMaster.Instance.Debug.AddQuickAction("1k sanity", () => _sanityPoints = 1000);
    //     }
    //
    //     private void Update()
    //     {
    //         DecreaseSanity(degradationRate * Time.deltaTime);
    //     }
    //
    //     public event Action OnMaxLevelChanged;
    //
    //     public void TakeDamage(DamageInfo damageInfo)
    //     {
    //         DecreaseSanity(damageInfo.Damage);
    //     }
    //
    //     private void DecreaseSanity(float value)
    //     {
    //         _sanityPoints -= value;
    //         if (_sanityPoints <= 0)
    //         {
    //             OnDead?.Invoke();
    //             return;
    //         }
    //
    //         if (_sanityPoints <= _nextLevelMaxSanityPoints)
    //         {
    //             _currentSanityLevel++;
    //             _currentMaxSanityPoints = maxSanityPoints[_currentSanityLevel];
    //             _nextLevelMaxSanityPoints = _currentSanityLevel + 1 < maxSanityPoints.Length
    //                 ? maxSanityPoints[_currentSanityLevel + 1]
    //                 : 0;
    //             OnMaxLevelChanged?.Invoke();
    //         }
    //     }
    // }
}