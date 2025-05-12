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

        public IEnumerator Execute(PlayerController target)
        {
            _token = new CancellationTokenSource();

            while (!_token.IsCancellationRequested)
            {
                if ((_container.transform.position.x < target.transform.position.x &&
                     _container.Renderer.CurrentScale < 0) ||
                    (_container.transform.position.x > target.transform.position.x &&
                     _container.Renderer.CurrentScale > 0))
                {
                    _container.Animator.SetBool("backAttack", true);
                    yield return new WaitForSeconds(_info.BackAttackStartDelay);
                }
                else
                {
                    _container.Animator.SetBool("attack", true);
                    yield return new WaitForSeconds(_info.AttackStartDelay);
                }

                if (!_token.IsCancellationRequested)
                    _container.Attack.Attack();
                else
                    yield break;

                yield return new WaitForSeconds(_info.AttackDelay);
            }
        }

        public void Stop()
        {
            _container.Animator.SetBool("backAttack", false);
            _container.Animator.SetBool("attack", false);
            _token.Cancel();
        }
    }
}