#region

using System;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace EnemySystem
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController : MonoBehaviour
    {
        private const float TRANSITION_DURATION = 0.1f;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int ReactionHit = Animator.StringToHash("ReactionHit");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Death2 = Animator.StringToHash("Death2");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Scream = Animator.StringToHash("Scream");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Walk = Animator.StringToHash("Walk");
        [SerializeField] private Enemy enemy;
        private Animator _animator;
        private int _currentState;
        private IEnemyController _enemyController;
        private float _lockedTill;

        private float _reactionAnimationDuration;
        private float _attackAnimationDuration;

        private void Awake()
        {
            _enemyController = enemy.GetComponent<IEnemyController>();
            _animator = GetComponent<Animator>();
        }

        public event EventHandler OnAttackAnimationFinished; 

        private void Start()
        {
            UpdateAnimClipTimes();
        }

        private void Update()
        {
            var state = GetState();

            if (_currentState == state)
            {
                if (_currentState == ReactionHit && _enemyController.IsHit)
                {
                    _animator.CrossFade(state, TRANSITION_DURATION, 0, 0.1f);
                }
            }
            else
            {
                _animator.CrossFade(state, TRANSITION_DURATION, 0);
                _currentState = state;
            }
        }

        private void UpdateAnimClipTimes()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                switch (clip.name)
                {
                    case "Zombie Reaction Hit":
                        _reactionAnimationDuration = clip.length;
                        break;
                    case "zombieAttack":
                        _attackAnimationDuration = clip.length;
                        break;
                }
            }
        }

        private int GetState()
        {
            if (_enemyController.IsDead)
            {
                if (Random.value > 0.5f || _currentState == Death)
                {
                    return Death;
                }

                return Death;
            }

            if (_enemyController.IsHit)
            {
                return LockState(ReactionHit, _reactionAnimationDuration);
            }

            if (Time.time < _lockedTill)
            {
                return _currentState;
            }

            if (_currentState == Attack)
            {
                OnAttackAnimationFinished?.Invoke(this, EventArgs.Empty);
            }


            if (_enemyController.IsAttacking)
            {
                return LockState(Attack, _attackAnimationDuration);
            }
            if (_enemyController.IsChasing)
            {
                return Run;
            }

            return Walk;

            int LockState(int s, float t)
            {
                _lockedTill = Time.time + t;
                return s;
            }
        }
    }
}