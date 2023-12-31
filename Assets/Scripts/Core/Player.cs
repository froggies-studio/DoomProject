using System;
using EnemySystem;
using StatsSystem;
using UI;
using UnityEngine;
using WeaponSystem;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        [Space(10)]
        [Header("Weapon systems")]
        [SerializeField] private Weapon weapon;
        [SerializeField] private RayGun rayGun;
        [SerializeField] private FiringPartRotation firingPartRotation;
        [SerializeField] private WeaponSoundController weaponSoundController;
         
        [Space(10)]
        [Header("UI systems")]
        [SerializeField] private BulletsUIComponent bulletsUIComponent;

        [SerializeField] private PlayerHurtResponder hurtResponder;
        private WeaponSystemController _weaponSystemController;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            _weaponSystemController = new WeaponSystemController(weapon, rayGun, bulletsUIComponent, firingPartRotation, weaponSoundController);
            hurtResponder.OnDamageReceived += HurtResponderOnDamageReceived;
        }

        private void HurtResponderOnDamageReceived(object sender, ReceivedDamageEventArgs e)
        {
            SanityController.Instance.DecreaseSanity(e.damageReceived);
        }

        private void Update()
        {
            _weaponSystemController.UpdateWeaponSystem();
        }
    }
}