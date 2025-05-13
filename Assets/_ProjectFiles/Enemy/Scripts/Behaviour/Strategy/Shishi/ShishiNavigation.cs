using System.Collections;
using System.Threading;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Shishi
{
    public class ShishiNavigation : INavigationExecutable
    {
        private readonly EnemyContainer _enemyContainer;
        private readonly ShishiNavigationInfo _navigationInfo;
        private CancellationTokenSource _token;

        public ShishiNavigation(EnemyContainer container)
        {
            _enemyContainer = container;
            _navigationInfo = container.Profile.NavigationInfo as ShishiNavigationInfo;
        }

        public IEnumerator Execute(PlayerController target)
        {
            _token = new CancellationTokenSource();

            while (!_token.IsCancellationRequested)
            {
                var targetDelta = _enemyContainer.transform.position.x - target.transform.position.x;
                var escapeDirection = targetDelta / Mathf.Abs(targetDelta);
                _enemyContainer.Rigidbody.velocity = new Vector2(escapeDirection, 0) * _navigationInfo.MoveSpeed;
                yield return null;
            }
        }

        public void Stop()
        {
            _token.Cancel();
            _enemyContainer.Rigidbody.velocity = Vector2.zero;
        }
    }
}