using System.Collections;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Hundun;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public class HundunNavigationExecutable : INavigationExecutable
    {
        private readonly HundunNavigationInfo _info;
        private readonly Rigidbody2D _rigidbody;
        private readonly AnimationCurve _speedCurve;
        private bool _isActive;

        public HundunNavigationExecutable(EnemyContainer container, EnemyNavigationInfo info)
        {
            _rigidbody = container.GetComponent<Rigidbody2D>();
            _info = info as HundunNavigationInfo;
            _speedCurve = _info.SpeedCurve;
        }

        bool INavigationExecutable.IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public IEnumerator Execute(Transform target)
        {
            _isActive = true;
            yield return new WaitForSeconds(_info.StartDashDelay);

            while (true)
            {
                if (!_isActive) yield break;
                        
                Vector2 currentTargetPosition = target.position;
                Vector2 moveDirection = (target.transform.position - _rigidbody.transform.position).normalized;
                var moveTime = _info.MoveSpeed; // in fact it's dash time, not move speed
                var distanceToTarget = Vector2.Distance(currentTargetPosition, _rigidbody.transform.position);
                var velocityMagnitude = distanceToTarget / _speedCurve.FunctionSquare(100);
                var time = 0f;

                while (time < moveTime)
                {
                    time += Time.deltaTime;

                    var currentVelocity = velocityMagnitude * _speedCurve.Evaluate(time) * moveDirection;
                    _rigidbody.velocity = currentVelocity;

                    yield return null;
                }

                yield return new WaitForSeconds(_info.DashTimeInterval);
            }
        }

        public void Stop()
        {
            _isActive = false;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}