using System.Collections;
using System.Linq;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Rendering;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Shishi
{
    public class ShishiAttack : IAttackExecutable
    {
        private readonly EnemyContainer _container;
        private readonly ShishiAttackInfo _attackInfo;
        private bool _isAttacking;
        
        public ShishiAttack(EnemyContainer container)
        {
            _container = container;
            _attackInfo = container.Profile.AttackInfo as ShishiAttackInfo;
        }

        public bool IsAttacking
        {
            get => _isAttacking;
            set => _isAttacking = value;
        }

        public IEnumerator Execute(Transform target)
        {
            _container.Animator.SetBool("attack", true);
            _container.Rigidbody.velocity = Vector2.zero;
            
            while (true)
            {
                _isAttacking = true;
                var missileSpeed = _attackInfo.MissileSpeed;
                var missileFireTime = _attackInfo.MissileFireTime;
                var shootDirection = _container.Renderer.CurrentScale;
            
                float currentTime = 0;
                var currentMissilePosition = _container.transform.position.x;

                while (currentTime < missileFireTime)
                {
                    currentTime += Time.deltaTime;
                    currentMissilePosition = _container.transform.position.x + currentTime * missileSpeed * shootDirection;
                    var overlap = Physics2D.OverlapCircleAll(new Vector2(currentMissilePosition, _container.transform.position.y), _attackInfo.MissileRadius);
                
                    var targetCollider = overlap.FirstOrDefault(x => x.gameObject.layer == LayerMask.NameToLayer("Player"));

                    if (targetCollider != null)
                    {
                        _container.Attack.Attack();
                        _isAttacking = false;
                        break;
                    }

                    yield return null;
                }

                _isAttacking = false;
                _container.Animator.SetBool("attackDelay", true);
                yield return new WaitForSeconds(_attackInfo.AttackDelay);
                _container.Animator.SetBool("attackDelay", false);
            }
            
        }

        public void Stop()
        {
            _container.Animator.SetBool("attack", false);
            _container.Animator.SetBool("attackDelay", false);
            _container.Attack.StopAllCoroutines();
            _container.Rigidbody.velocity = Vector2.zero;
        }
    }
}