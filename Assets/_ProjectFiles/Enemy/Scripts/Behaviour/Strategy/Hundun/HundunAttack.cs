using System.Collections;
using System.Threading;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Hundun;
using _ProjectFiles.SoundContainer;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public class HundunAttack : IAttackExecutable
    {
        private readonly EnemyContainer _container;
        private readonly HundunAttackInfo _info;
        private CancellationTokenSource _token;

        public HundunAttack(EnemyContainer container)
        {
            _container = container;
            _info = container.Profile.AttackInfo as HundunAttackInfo;
        }

        public IEnumerator Execute(PlayerController target)
        {
            _token = new CancellationTokenSource();

            while (!_token.IsCancellationRequested)
            {
                var targetDelta = target.transform.position - _container.transform.position;
                _container.Renderer.CurrentScale = targetDelta.x > 0 ? 1 : -1;

                _container.Animator.SetTrigger("attack");
                yield return new WaitForSeconds(_info.AttackStartDelay);
                if (_token.IsCancellationRequested) yield break;

                _container.Attack.Attack();
                _container.Audio.PlaySoundEffect(SoundType.HundunAttack);

                yield return new WaitForSeconds(_info.AttackDelay);
            }
        }

        public void Stop()
        {
            _token.Cancel();
            _container.Animator.SetTrigger("idle");
        }
    }
}