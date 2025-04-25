using System.Collections;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Shishi
{
    public class ShishiNavigation : INavigationExecutable
    {
        private EnemyContainer _enemyContainer;
        private ShishiNavigationInfo _navigationInfo;
        
        public ShishiNavigation(EnemyContainer container)
        {
            _enemyContainer = container;
            _navigationInfo = container.Profile.NavigationInfo as ShishiNavigationInfo;
        }
        
        public IEnumerator Execute(Transform target)
        {
            var targetDelta =  _enemyContainer.transform.position.x - target.transform.position.x;
            var escapeDirection = targetDelta / Mathf.Abs(targetDelta);
            _enemyContainer.Rigidbody.velocity = new Vector2(escapeDirection, 0) * _navigationInfo.MoveSpeed;
            yield break; 
        }

        public void Stop()
        {
            _enemyContainer.Rigidbody.velocity = Vector2.zero;
        }
    }
}