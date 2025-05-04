using System.Collections;
using System.Threading;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Kirin;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public class KirinAttack : IAttackExecutable
    {
        private readonly EnemyContainer _container;
        private readonly KirinAttackInfo _info;
        private CancellationTokenSource _token;

        public KirinAttack(EnemyContainer container)
        {
            _container = container;
            _info = container.Profile.AttackInfo as KirinAttackInfo;
        }

        public IEnumerator Execute(Transform target)
        {
            while (true)
            {
                if ((_container.transform.position.x < target.transform.position.x &&
                     _container.Renderer.CurrentScale < 0) || (_container.transform.position.x > target.transform.position.x && _container.Renderer.CurrentScale > 0))
                {
                    _container.Animator.SetTrigger("backAttack");
                    yield return new WaitForSeconds(_info.BackAttackStartDelay);
                }
                else
                {
                    _container.Animator.SetTrigger("attack");
                    yield return new WaitForSeconds(_info.AttackStartDelay);
                }
                
                _container.Attack.Attack();
                yield return new WaitForSeconds(_info.AttackDelay);
            }
        }

        public void Stop()
        {
            _container.Attack.StopAllCoroutines();
        }
    }
}